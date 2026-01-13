using UnityEngine;
using UnityEngine.InputSystem;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private ObjectBoost _velocityBoost;
    [SerializeField, Range(0f, 100f)] private float _defaultAcceleration = 50f;
    [SerializeField, Range(0f, 100f)] private float _boosterAcceleration = 100f;
    [SerializeField, Range(0f, 100f)] private float _maxSpeed = 6f;
    [SerializeField, Range(0f, 500f)] private float _rotationSpeed = 6f;

    private float _moveInput;
    private bool IsBoosting => _moveInput == 1f ? true : false;
    private float _rotateInput;

    public void FixedUpdate()
    {
        ApplyMove();
        ApplyRotate();
    }

    private void ApplyMove()
    {
        Vector3 maximalVelocity = transform.forward * _maxSpeed;
        Vector3 currentVelocity = _rigidbody.velocity;
        float verticalSpeed = currentVelocity.y;
        currentVelocity.y = 0f;

        var currentAcceleration = IsBoosting && _velocityBoost.TryBoost() ? _boosterAcceleration : _defaultAcceleration;
        float deltaAcceleration = currentAcceleration * Time.fixedDeltaTime;

        currentVelocity = Vector3.MoveTowards(currentVelocity, maximalVelocity, deltaAcceleration);
        currentVelocity.y = verticalSpeed;
        _rigidbody.velocity = currentVelocity;
    }

    private void ApplyRotate()
    {
        float rotation = _rotateInput * _rotationSpeed * Time.fixedDeltaTime;
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, rotation, 0));
    }

    public void Move(InputAction.CallbackContext value)
    {
        _moveInput = value.ReadValue<float>();
    }

    public void Rotate(InputAction.CallbackContext value)
    {
        _rotateInput = value.ReadValue<float>();
    }
}
