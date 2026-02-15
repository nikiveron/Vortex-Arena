using System.Collections;
using TMPro;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanelController : PanelController
{
    [SerializeField] private SceneSwitcher _sceneSwitcher;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private SecureScoreManager _secureScoreManager;
    [SerializeField] private BulletCounter _bulletsCounter;
    [SerializeField] private Animator _animator;

    [Header("Audio")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _looseClip;
    [SerializeField] private AudioClip _winClip;

    [Header("Current player score")]
    [SerializeField] private TMP_Text _score;
    [SerializeField] private TMP_Text _time;
    [SerializeField] private TMP_Text _currentBullets;

    [Header("Leaderboard")]
    [SerializeField] private TMP_Text[] _scores;
    [SerializeField] private TMP_Text[] _times;
    [SerializeField] private TMP_Text[] _bullets;
    [SerializeField] private TMP_Text[] _dates;

    [Header("Bottom buttons")]
    [SerializeField] private Button _restart;
    [SerializeField] private Button _menu;

    private void Awake()
    {
        _restart.onClick.AddListener(RestartGame);
        _menu.onClick.AddListener(ExitGame);
        _audioSource.clip = _looseClip;
    }

    public override void ShowPanel()
    {
        base.ShowPanel();
        _score.text = $"{_scoreCounter.Score}";
        _time.text = GameTimer.FormattedTime();
        _currentBullets.text = $"{_bulletsCounter.Count}";

        var leaderBoard = _secureScoreManager.LoadScoreData();
        for (int i = 0; i < leaderBoard.bestScores.Count; i++)
        {
            var score = leaderBoard.bestScores[i];

            _scores[i].text = $"{score.score}";
            _times[i].text = GameTimer.FormattedTime(score.playTimeSeconds);
            _bullets[i].text = $"{score.bulletSpent}";
            string[] dateAndTime = score.dateTime.Split(' ');
            _dates[i].text = $"{dateAndTime[0]}\n{dateAndTime[1]}";
        }

        if (_secureScoreManager.LastScoreWasBest)
        {
            StartCoroutine(PlayWinEffectsNextFrame());
        }
        else
        {
            _audioSource.PlayOneShot(_looseClip);
        }
    }

    private IEnumerator PlayWinEffectsNextFrame()
    {
        yield return null; 

        _audioSource.PlayOneShot(_winClip);
        _animator.ResetTrigger("Appear");
        _animator.SetTrigger("Appear");
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