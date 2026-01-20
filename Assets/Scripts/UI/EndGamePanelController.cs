using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelController : PanelController
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _menu.onClick.AddListener(ExitGame);
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _score.text = $"{ScoreCounter.Score}";
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