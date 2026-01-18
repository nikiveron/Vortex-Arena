using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private string _mainMenuSceneName = "01_MainMenu";
    [SerializeField] private string _gameMenuSceneName = "02_GameLevel";
    
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(_mainMenuSceneName);
    }

    public void LoadGameLevel()
    {
        _pauseManager.ResumeGame();
        SceneManager.LoadScene(_gameMenuSceneName);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
