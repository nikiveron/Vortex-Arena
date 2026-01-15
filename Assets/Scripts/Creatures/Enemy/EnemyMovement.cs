using UnityEngine;

public class EnemyMovement : ObjectMovement
{
    [SerializeField] private float _rotationSpeed = 6f;

    public void Move(Vector3 direction)
    {
        Vector3 maximalVelocity = direction * _maxSpeed;
        Vector3 currentVelocity = _rigidbody.velocity;
        float verticalSpeed = currentVelocity.y;
        currentVelocity.y = 0f;

        float currentAcceleration = _defaultAcceleration;
        float deltaAcceleration = currentAcceleration * Time.fixedDeltaTime;

        currentVelocity = Vector3.MoveTowards(currentVelocity, maximalVelocity, deltaAcceleration);
        currentVelocity.y = verticalSpeed;
        _rigidbody.velocity = currentVelocity;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        _rigidbody.rotation = Quaternion.Slerp(_rigidbody.rotation, targetRotation, _rotationSpeed * Time.fixedDeltaTime);
    }
}