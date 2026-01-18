using UnityEngine;

public class EnemyDeathHandler : ObjectDeathHandler
{
    [SerializeField] private int _killPoints = 1;

    public override void HandleDestroyed()
    {
        EnemyEvents.NotifyEnemyKilled(_killPoints);
        base.HandleDestroyed();
    }
}