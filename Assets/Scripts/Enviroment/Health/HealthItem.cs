using UnityEngine;

public class HealthItem : MonoBehaviour
{
    [SerializeField] private int _healthPoints = 10;
    private bool _isUsed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_isUsed) return;
        if (other.attachedRigidbody.TryGetComponent<HealthRecovery>(out var healthRecovery))
        {
            _isUsed = true;
            healthRecovery.AddHealth(_healthPoints);
            Destroy(gameObject);
        }
    }
}
