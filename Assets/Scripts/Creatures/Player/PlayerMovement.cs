using UnityEngine;

public class PlayerMovement : ObjectMovement
{
    [SerializeField] protected ObjectBoost _velocityBoost;

    public void Move(Vector3 direction)
    {
        Vector3 maximalVelocity = direction * _maxSpeed;
        Vector3 currentVelocity = _rigidbody.velocity;
        float verticalSpeed = currentVelocity.y;
        currentVelocity.y = 0f;

        float currentAcceleration = _velocityBoost.TryBoost() ? _boosterAcceleration : _defaultAcceleration;
        float deltaAcceleration = currentAcceleration * Time.fixedDeltaTime;

        currentVelocity = Vector3.MoveTowards(currentVelocity, maximalVelocity, deltaAcceleration);
        currentVelocity.y = verticalSpeed;
        _rigidbody.velocity = currentVelocity;
    }
}