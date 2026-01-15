using System;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ObjectBoost _velocityBoost;
    [SerializeField, Range(0f, 100f)] private float _defaultAcceleration = 50f;
    [SerializeField, Range(0f, 100f)] private float _boosterAcceleration = 100f;
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 6f;

    public void Move(float moveInput)
    {
        Vector3 maximalVelocity = transform.forward * _maxSpeed;
        Vector3 currentVelocity = _rigidbody.velocity;
        float verticalSpeed = currentVelocity.y;
        currentVelocity.y = 0f;

        var currentAcceleration = IsBoosting(moveInput) && _velocityBoost.TryBoost() ? _boosterAcceleration : _defaultAcceleration;
        float deltaAcceleration = currentAcceleration * Time.fixedDeltaTime;

        currentVelocity = Vector3.MoveTowards(currentVelocity, maximalVelocity, deltaAcceleration);
        currentVelocity.y = verticalSpeed;
        _rigidbody.velocity = currentVelocity;
    }

    private bool IsBoosting(float moveInput)
    {
        return moveInput == 1f;
    }
}
