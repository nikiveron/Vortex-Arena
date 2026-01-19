using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text _timerText;
    [SerializeField] private static string _timeFormat = "mm\\:ss";
    [SerializeField] private static bool _showMilliseconds = false;

    [Header("Timer Settings")]
    [SerializeField] private bool _startOnAwake = true;
    [SerializeField] private bool _pauseOnGamePause = true;

    private static float _elapsedTime = 0f;
    private bool _isRunning = false;

    public static float ElapsedTime => _elapsedTime;
    public bool IsRunning => _isRunning;

    private void Start()
    {
        if (_timerText == null)
            _timerText = GetComponent<TMP_Text>();

        if (_startOnAwake)
            StartTimer();
    }

    private void Update()
    {
        if (!_isRunning) return;
        _elapsedTime += Time.deltaTime;
        UpdateTimerDisplay();
    }

    private void UpdateTimerDisplay()
    {
        if (_timerText == null) return;

        _timerText.text = FormattedTime();
    }

    public static string FormattedTime()
    {
        if (_showMilliseconds)
        {
            int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(_elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt((_elapsedTime * 1000) % 1000);

            return string.Format("{0:00}:{1:00}.{2:000}", minutes, seconds, milliseconds);
        }
        else
        {
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(_elapsedTime);
            return timeSpan.ToString(_timeFormat);
        }
    }

    public void StartTimer()
    {
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public void ResetTimer()
    {
        _elapsedTime = 0f;
        UpdateTimerDisplay();
    }

    public void RestartTimer()
    {
        ResetTimer();
        StartTimer();
    }

    private void OnEnable()
    {
        if (_pauseOnGamePause)
        {
            PauseManager.OnGamePaused += StopTimer;
            PauseManager.OnGameResumed += StartTimer;
        }
    }

    private void OnDisable()
    {
        if (_pauseOnGamePause)
        {
            PauseManager.OnGamePaused -= StopTimer;
            PauseManager.OnGameResumed -= StartTimer;
        }
    }
}