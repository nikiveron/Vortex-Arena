using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private PanelController _helpPanel;
    [SerializeField] private PanelController _aboutPanel;

    [SerializeField] private Button _play;
    [SerializeField] private Button _help;
    [SerializeField] private Button _settings;
    [SerializeField] private Button _about;
    [SerializeField] private Button _exit;

    private void Awake()
    {
        _helpPanel.HidePanel();
        _aboutPanel.HidePanel();

        _play.onClick.AddListener(StartPlay);
        _help.onClick.AddListener(_helpPanel.ShowPanel);
        _about.onClick.AddListener(_aboutPanel.ShowPanel);
        _exit.onClick.AddListener(ExitGame);
    }

    private void StartPlay()
    {
        _sceneSwitcher.LoadGameLevel();
    }

    private void ExitGame()
    {
        _sceneSwitcher.ExitGame();
    }
}
