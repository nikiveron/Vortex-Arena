using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelController : PanelController
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private PauseManager _pauseManager;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;

    private void Awake()
    {
        _restart.onClick.AddListener(ResumeGame);
        _menu.onClick.AddListener(ExitGame);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _score.text = $"{ScoreCounter.Score}";
    }

    private void ResumeGame()
    {
        _pauseManager.ResumeGame();
        _sceneSwitcher.LoadGameLevel();
    }

    private void ExitGame()
    {
        _sceneSwitcher.LoadMainMenu();
    }
}