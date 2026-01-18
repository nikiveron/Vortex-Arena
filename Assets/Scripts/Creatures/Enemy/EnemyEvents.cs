using UnityEngine;
using System;

public static class EnemyEvents
{
    public static event Action<int> OnEnemyKilled;

    public static void NotifyEnemyKilled(int points)
    {
        OnEnemyKilled?.Invoke(points);
    }
}