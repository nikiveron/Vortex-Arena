using UnityEngine;

public class DealDamageAndDestroy : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _damage = 10f;
    private bool _isPlayer = false;

    public void Initialize()
    {
        if (_bullet.ShooterType == CreatureType.Player)
        {
            _isPlayer = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out _) ^ _isPlayer)
        {
            if (collision.gameObject.TryGetComponent<ObjectHealth>(out var health))
            {
                health.TakeDamage(_damage);
            }
            Destroy(gameObject);
        }
    }
}