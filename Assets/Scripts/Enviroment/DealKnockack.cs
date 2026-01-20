using UnityEngine;

public class DealKnockback : MonoBehaviour
{
    [SerializeField] private float _knockbackForce = 5f; 
    [SerializeField] private Vector3 _centerPoint = Vector3.zero;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            Vector3 direction = (_centerPoint - collision.transform.position).normalized;
            collision.rigidbody.AddForce(direction * _knockbackForce, ForceMode.Impulse);
        }
    }
}
