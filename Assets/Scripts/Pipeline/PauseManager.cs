using UnityEngine;

public class PauseManager : MonoBehaviour
{
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
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f; 
        _isPaused = false;
    }

    public bool IsPaused()
    {
        return _isPaused;
    }
}