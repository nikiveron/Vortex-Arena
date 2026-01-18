using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathHandler : ObjectDeathHandler
{
    [SerializeField] private GameLevelUIController _gameLevelUIController;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private float _delayBeforeGameOver;

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
        yield return new WaitForSeconds(_delayBeforeGameOver);
        _gameLevelUIController.EndGame();
    }
}