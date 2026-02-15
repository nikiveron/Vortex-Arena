using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class EnvLoader
{
    private static Dictionary<string, string> _envVariables;

    public static void LoadEnv()
    {
        if (_envVariables != null) return;

        _envVariables = new Dictionary<string, string>();

        // Путь к .env файлу (рядом с Assets)
        string envPath = Path.Combine(Directory.GetParent(Application.dataPath).FullName, ".env");

        if (!File.Exists(envPath))
        {
            Debug.LogError($".env file not found at {envPath}");
            return;
        }

        try
        {
            string[] lines = File.ReadAllLines(envPath);

            foreach (string line in lines)
            {
                string trimmedLine = line.Trim();

                if (string.IsNullOrEmpty(trimmedLine) || trimmedLine.StartsWith("#"))
                    continue;

                int separatorIndex = trimmedLine.IndexOf('=');
                if (separatorIndex < 0) continue;

                string key = trimmedLine[..separatorIndex].Trim();
                string value = trimmedLine[(separatorIndex + 1)..].Trim();

                if (value.StartsWith("\"") && value.EndsWith("\""))
                    value = value[1..^1]; // тоже самое что Substring(1, value.Length - 2)

                _envVariables[key] = value;
            }

            Debug.Log($".env file loaded successfully with {_envVariables.Count} variables");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error loading .env file: {e.Message}");
        }
    }

    public static string GetEnv(string key, string defaultValue = null)
    {
        LoadEnv(); 

        if (_envVariables != null && _envVariables.TryGetValue(key, out string value))
            return value;

        if (defaultValue != null)
            return defaultValue;

        Debug.LogWarning($"Environment variable {key} not found and no default provided");
        return null;
    }

}