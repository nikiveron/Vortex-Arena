using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLevelUIController : MonoBehaviour
{
	[SerializeField] private SceneSwitcher _sceneSwitcher;
	[SerializeField] private PauseManager _pauseManager;
	[SerializeField] private AudioSource _backgroundMusic;
	[SerializeField] private PanelController _menuPanel;
	[SerializeField] private PanelController _helpPanel;
	[SerializeField] private EndGamePanelController _endGamePanel;

	[SerializeField] private Button _stopGame;
	[SerializeField] private Button _return;
	[SerializeField] private Button _restart;
	[SerializeField] private Button _help;
	[SerializeField] private Button _exit;

	private void Awake()
	{
        _menuPanel.HidePanel();
        _helpPanel.HidePanel();
        _endGamePanel.HidePanel();

        _stopGame.onClick.AddListener(StopGame);
		_restart.onClick.AddListener(RestartGame);
        _return.onClick.AddListener(ResumeGame);
		_help.onClick.AddListener(_helpPanel.ShowPanel);
		_exit.onClick.AddListener(ExitGame);
	}

	private void StopGame()
	{
		_pauseManager.PauseGame();
		_backgroundMusic.Pause();
        _menuPanel.ShowPanel();
    }

	private void RestartGame()
	{
        _sceneSwitcher.LoadGameLevel();
    }

    private void ResumeGame()
	{
		_pauseManager.ResumeGame();
		_backgroundMusic.Play();
        _menuPanel.HidePanel();
    }

	private void ExitGame()
	{
		_sceneSwitcher.LoadMainMenu();
	}

	public void EndGame()
    {
        _pauseManager.PauseGame(); 
		_backgroundMusic.Pause();
        _endGamePanel.ShowPanel();
    }

    private void OnDisable()
    {
        _stopGame.onClick.RemoveAllListeners();
        _return.onClick.RemoveAllListeners();
        _help.onClick.RemoveAllListeners();
        _exit.onClick.RemoveAllListeners();
    }
}
