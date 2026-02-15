using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class SecureScoreManager : MonoBehaviour
{
    [SerializeField] private SecretConfig _secretConfig;

    private bool _lastScoreWasBest;
    public bool LastScoreWasBest => _lastScoreWasBest;

    private string _savePath;
    private static readonly string _folderName = "Scores";
	private readonly string _fileName = "scores.dat";
	private byte[] _encryptionKey;
	private byte[] _iv;
	private string _secret = string.Empty;

    private void Awake()
    {
        InitializeSavePath();
        InitializeSecret();
        InitializeEncryptionKey();
        InitializeIV();
	}

    private void InitializeSavePath()
    {
        _savePath = Path.Combine(GetSavePath(), _fileName);
        string directory = Path.GetDirectoryName(_savePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    private void InitializeSecret()
    {
        if (_secretConfig == null)
        {
            Debug.LogError("SecretConfig not assigned! Using fallback (INSECURE)");
            _secret = "INSECURE_FALLBACK_SALT";
            return;
        }

        _secret = _secretConfig.SecretSalt;
    }

    private void InitializeEncryptionKey()
    {
        // Создаем уникальный ключ на основе железа
        string machineKey = SystemInfo.deviceUniqueIdentifier;
        using SHA256 sha256 = SHA256.Create();
        _encryptionKey = sha256.ComputeHash(Encoding.UTF8.GetBytes(machineKey + _secret));
    }

    public static string GetSavePath()
    {
#if UNITY_EDITOR
        return Path.Combine(Directory.GetParent(Application.dataPath).FullName, _folderName);
#elif UNITY_STANDALONE_WIN
            // Windows: C:\Users\[User]\AppData\LocalLow\[Company]\[Game]\Scores
            return Path.Combine(Application.persistentDataPath, _folderName);
#elif UNITY_STANDALONE_LINUX
            // Linux: ~/.config/unity3d/[Company]/[Game]/Scores
            return Path.Combine(Application.persistentDataPath, _folderName);
#elif UNITY_STANDALONE_OSX
            // Mac: ~/Library/Application Support/[Company]/[Game]/Scores
            return Path.Combine(Application.persistentDataPath, _folderName);
#else
            return Application.persistentDataPath;
#endif
    }

    private void InitializeIV()
	{
		if (PlayerPrefs.HasKey("iv"))
		{
			string ivString = PlayerPrefs.GetString("iv");

			string[] hexValues = ivString.Split(' ');
			_iv = new byte[hexValues.Length];

			for (int i = 0; i < hexValues.Length; i++)
			{
				_iv[i] = Convert.ToByte(hexValues[i], 16);
			}

			Debug.Log("IV loaded from PlayerPrefs");
		}
		else
		{
			using (var rng = new RNGCryptoServiceProvider())
			{
				_iv = new byte[16];
				rng.GetBytes(_iv);
			}

			string[] hexStrings = new string[_iv.Length];
			for (int i = 0; i < _iv.Length; i++)
			{
				hexStrings[i] = "0x" + _iv[i].ToString("X2");
			}

			string ivString = string.Join(" ", hexStrings);

			PlayerPrefs.SetString("iv", ivString);
			PlayerPrefs.Save();

			Debug.Log("New IV generated and saved to PlayerPrefs");
		}
	}

	private string CalculateChecksum(ScoreRecord record)
	{
		string data = $"{record.playTimeSeconds}|{record.bulletSpent}|{record.score}|{record.dateTime}|{_secret}";
        using SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
        return Convert.ToBase64String(hash);
    }

	private bool VerifyChecksum(ScoreRecord record)
	{
		if (string.IsNullOrEmpty(record.checksum))
			return false;

		string calculatedChecksum = CalculateChecksum(record);
		return calculatedChecksum == record.checksum;
	}

    private byte[] EncryptData(string plainText)
    {
        using Aes aes = Aes.Create();
        aes.Key = _encryptionKey;
        aes.IV = _iv;

        ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

        using MemoryStream ms = new();
        using CryptoStream cs = new (ms, encryptor, CryptoStreamMode.Write);
        using (StreamWriter sw = new (cs))
        {
            sw.Write(plainText);
        }
        return ms.ToArray();
    }

    private string DecryptData(byte[] cipherText)
    {
        using Aes aes = Aes.Create();
        aes.Key = _encryptionKey;
        aes.IV = _iv;

        ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

        using MemoryStream ms = new (cipherText);
        using CryptoStream cs = new (ms, decryptor, CryptoStreamMode.Read);
        using StreamReader sr = new (cs);
        return sr.ReadToEnd();
    }

    public void SubmitScore(float playTime, int bulletsSpent, int score)
    {
        var newRecord = new ScoreRecord
        {
            playTimeSeconds = playTime,
            bulletSpent = bulletsSpent,
            score = score,
            dateTime = DateTime.UtcNow.ToLocalTime().ToString("g"),
        };

        SaveScore(newRecord);
    }

    private void SaveScore(ScoreRecord newRecord)
    {
        ScoreData scoreData = LoadScoreData();

        bool wasBest = false;
        if (scoreData.bestScores.Count == 0)
        {
            wasBest = true;
        }
        else
        {
            int currentBest = scoreData.bestScores.Max(r => r.score);
            if (newRecord.score > currentBest)
                wasBest = true;
        }

        newRecord.checksum = CalculateChecksum(newRecord);

        scoreData.bestScores.Add(newRecord);
        scoreData.bestScores = scoreData.bestScores
            .OrderByDescending(r => r.score)
            .ToList();

        if (scoreData.bestScores.Count > 3)
        {
            scoreData.bestScores = scoreData.bestScores.Take(3).ToList();
        }

        string json = JsonUtility.ToJson(scoreData, true);
        byte[] encryptedData = EncryptData(json);
        byte[] dataWithHash = AddIntegrityCheck(encryptedData);     // Добавляем дополнительный хэш для проверки целостности
        File.WriteAllBytes(_savePath, dataWithHash);

        _lastScoreWasBest = wasBest;
    }

    private byte[] AddIntegrityCheck(byte[] data)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hash = sha256.ComputeHash(data);
        byte[] result = new byte[data.Length + hash.Length];
        Array.Copy(data, result, data.Length);
        Array.Copy(hash, 0, result, data.Length, hash.Length);
        return result;
    }

    private bool VerifyIntegrity(byte[] dataWithHash)
    {
        if (dataWithHash.Length < 32) return false;

        byte[] data = new byte[dataWithHash.Length - 32];
        byte[] hash = new byte[32];

        Array.Copy(dataWithHash, data, data.Length);
        Array.Copy(dataWithHash, data.Length, hash, 0, 32);

        using SHA256 sha256 = SHA256.Create();
        byte[] calculatedHash = sha256.ComputeHash(data);
        return calculatedHash.SequenceEqual(hash);
    }

    public ScoreData LoadScoreData()
    {
        if (!File.Exists(_savePath))
            return new ScoreData();

        try
        {
            byte[] dataWithHash = File.ReadAllBytes(_savePath);

            // Проверка целостности
            if (!VerifyIntegrity(dataWithHash))
            {
                Debug.LogWarning("Score file integrity check failed! Loading default.");
                return new ScoreData();
            }

            // Извлекаем данные без хэша
            byte[] encryptedData = new byte[dataWithHash.Length - 32];
            Array.Copy(dataWithHash, encryptedData, encryptedData.Length);

            // Расшифровываем
            string json = DecryptData(encryptedData);
            ScoreData scoreData = JsonUtility.FromJson<ScoreData>(json);

            if (scoreData.bestScores == null)
                scoreData.bestScores = new List<ScoreRecord>();

            scoreData.bestScores = scoreData.bestScores
                .Where(r => VerifyChecksum(r))
                .ToList();

            return scoreData;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to load scores: {e.Message}");
            return new ScoreData();
        }
    }
}