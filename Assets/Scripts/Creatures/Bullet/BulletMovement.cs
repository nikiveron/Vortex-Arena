using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody; 
    [SerializeField] private float _speed = 5;
    
    private void Start()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        _rigidbody.AddForce(transform.forward * _speed, ForceMode.VelocityChange);
    }
}