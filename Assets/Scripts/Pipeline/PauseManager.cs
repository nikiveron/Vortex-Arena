using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static event Action OnGamePaused;
    public static event Action OnGameResumed;

    private bool _isPaused = false;

    public void TogglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            PauseGame();
        }
        else
        {
            ResumeGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        _isPaused = true;
        OnGamePaused?.Invoke();
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        _isPaused = false;
        OnGameResumed?.Invoke();
    }

    public bool IsPaused()
    {
        return _isPaused;
    }
}