using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody.TryGetComponent<HealthRecovery>(out var healthRecovery))
        {
            healthRecovery.AddHealth(_healthPoints);
            Destroy(gameObject);
        }
    }
}
