using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyShooter _shooter;
    [SerializeField, Range(0f, 100f)] private float _shooterDistance;
    [SerializeField] private float _stopDistance = 2.5f;
    public Transform Player { get; set; }

    private void Update()
    {
        PursuePlayer();
    }

    private void PursuePlayer()
    {
        Vector3 targetPosition = GetTargetPosition();
        if (GetDistanceToTarget(targetPosition) >= _stopDistance)
        {
            Vector3 directionToTarget = GetDirectionToTarget(targetPosition);
            MoveAndShoot(directionToTarget);
        }
    }

    private float GetDistanceToTarget(Vector3 targetPosition)
    {
        return Vector3.Distance(transform.position, targetPosition);    
    }

    private Vector3 GetTargetPosition()
    {
        if (Player != null)
        {
            return Player.position;
        }
        else
        {
            return transform.position;
        }

    }

    private Vector3 GetDirectionToTarget(Vector3 targetPosition)
    {
        Vector3 directionToTarget = targetPosition - transform.position;
        directionToTarget.y = 0f;
        directionToTarget = directionToTarget.normalized;
        return directionToTarget;
    }

    private void MoveAndShoot(Vector3 direction)
    {
        _movement.Move(direction);
        _shooter.Shoot(IsPlayerAround());
    }

    private bool IsPlayerAround()
    {
        float distance = Vector3.Distance(transform.position, GetTargetPosition());
        return distance <= _shooterDistance;
    }
}
