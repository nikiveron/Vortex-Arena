using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private ObjectMovement _movement;
    [SerializeField] private EnemyShooter _shooter;

    void Update()
    {
        PursuePlayer();
    }

    private void PursuePlayer()
    {
        //Vector3 targetPosition = _zoneDetector.GetTargetPosition();
        //Vector3 directionToTarget = GetDirectionToTarget(targetPosition);
        //MoveAndShoot(directionToTarget, true);
    }

    private Vector3 GetDirectionToTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position; directionToTarget.y = 0f; directionToTarget = directionToTarget.normalized;
        return directionToTarget;
    }

    private void MoveAndShoot(Vector3 direction)
    {
        //_movement.Move(direction);
    }
}
