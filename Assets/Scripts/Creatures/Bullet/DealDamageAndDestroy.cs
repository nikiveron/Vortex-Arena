using UnityEngine;

public class DealDamageAndDestroy : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;
    [SerializeField] private AudioClip _hitSound;
    [SerializeField] private float _damage = 10f;
    private bool _isPlayer = false; 
    private bool _hasHit = false;

    public void Initialize()
    {
        if (_bullet.ShooterType == CreatureType.Player)
        {
            _isPlayer = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_hasHit) return;

        if ((collision.gameObject.TryGetComponent<PlayerController>(out _) ^ _isPlayer) && collision.gameObject.TryGetComponent<ObjectHealth>(out var health))
        {
            _hasHit = true;
            health.TakeDamage(_damage);
            AudioSource.PlayClipAtPoint(_hitSound, transform.position, 1f);

            Destroy(gameObject);
        }
    }
}