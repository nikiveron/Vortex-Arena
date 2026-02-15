using System.Collections;
using UnityEngine;

public class PlayerDeathHandler : ObjectDeathHandler
{
    [SerializeField] private GameLevelUIController _gameLevelUIController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _delayBeforeGameOver;
    [SerializeField] private SecureScoreManager _secureScoreManager;
    [SerializeField] private ScoreCounter _scoreCounter;
    [SerializeField] private BulletCounter _bulletCounter;
    [SerializeField] private GameTimer _gameTimer;

    public override void HandleDestroyed()
    {
        DisableRenderer();
        DisablePhysics();
        DisableMovement();
        StartCoroutine(WaitAndLoadFatalScene());
    }

    private void DisableMovement()
    {
        _playerController.enabled = false;
    }

    private IEnumerator WaitAndLoadFatalScene()
    {
        _gameTimer.StopTimer();
        _secureScoreManager.SubmitScore(GameTimer.ElapsedTime, _bulletCounter.Count, _scoreCounter.Score);
        yield return new WaitForSeconds(_delayBeforeGameOver);
        _gameLevelUIController.EndGame();
    }
}