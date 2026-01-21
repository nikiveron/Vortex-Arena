using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRecovery : MonoBehaviour
{
    [SerializeField] private ObjectHealth _objectHealth;

    public void AddHealth(int points)
    {
        _objectHealth.AddHealth(points);
    }
}
