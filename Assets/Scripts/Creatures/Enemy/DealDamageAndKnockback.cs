using UnityEngine;

public class DealDamageAndKnockback : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _knockbackForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<ObjectHealth>(out var health)) health.TakeDamage(_damage);
        if (collision.rigidbody)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.rigidbody.AddForce(direction * _knockbackForce, ForceMode.Impulse);
        }
    }
}
