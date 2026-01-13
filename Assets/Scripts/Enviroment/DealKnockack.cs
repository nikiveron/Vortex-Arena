using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealKnockback : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 5f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            collision.rigidbody.AddForce(direction * _knockbackForce, ForceMode.Impulse);
        }
    }
}
