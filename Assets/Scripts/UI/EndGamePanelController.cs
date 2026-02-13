using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelController : PanelController
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;
    [SerializeField] private string _scoreStorageName = "HighestScore";

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _menu.onClick.AddListener(ExitGame);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _score.text = $"{_scoreCounter.Score}";
        _time.text = GameTimer.FormattedTime();
    }

    private void RestartGame()
    {
        _sceneSwitcher.LoadGameLevel();
    }

    private void ExitGame()
    {
        _sceneSwitcher.LoadMainMenu();
    }
}