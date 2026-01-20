using System.Collections.Generic;
using UnityEngine;

public class DealDamageAndKnockback : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _knockbackForce = 5f;

    private HashSet<GameObject> _damagedObjects = new HashSet<GameObject>();

    private void OnCollisionEnter(Collision collision)
    {
        if (_damagedObjects.Contains(collision.gameObject))
            return;

        if (collision.gameObject.TryGetComponent<ObjectHealth>(out var health) && collision.gameObject.TryGetComponent<PlayerController>(out _))
        {
            _damagedObjects.Add(collision.gameObject);
            health.TakeDamage(_damage);
        }
        if (collision.rigidbody)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.rigidbody.AddForce(direction * _knockbackForce, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        _damagedObjects.Remove(collision.gameObject);
    }
}
