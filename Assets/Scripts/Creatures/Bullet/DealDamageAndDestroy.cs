using UnityEngine;

public class DealDamageAndDestroy : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    private bool _hasHit = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit) return;

        if (collision.gameObject.TryGetComponent<ObjectHealth>(out var health))
        {
            _hasHit = true;
            health.TakeDamage(_damage);

        }
        Destroy(gameObject);
    }
}