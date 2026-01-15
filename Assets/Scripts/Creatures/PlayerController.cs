using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private ObjectMovement _objectMovement;
    [SerializeField] private ObjectRotate _objectRotate;
    [SerializeField] private ObjectShoot _objectShoot;
    [SerializeField] private string _moveActionName = "Move";
    [SerializeField] private string _rotateActionName = "Rotate";
    [SerializeField] private string _shootActionName = "Shoot";

    private InputAction _moveAction;
    private InputAction _rotateAction;
    private InputAction _shootAction;
    private float _inputMovementDirection;
    private float _inputRotateDirection;
    private bool _isShooting;

    private void OnEnable()
    {
        _moveAction?.Enable();
        _rotateAction?.Enable();
        _shootAction?.Enable();
    }

    private void Start()
    {
        _moveAction = _playerInput.actions[_moveActionName];
        _moveAction.performed += OnMove;
        _moveAction.canceled += OnMove;
        _moveAction.Enable();

        _rotateAction = _playerInput.actions[_rotateActionName];
        _rotateAction.performed += OnRotate;
        _rotateAction.canceled += OnRotate;
        _rotateAction.Enable();

        _shootAction = _playerInput.actions[_shootActionName];
        _shootAction.started += OnShoot;
        _shootAction.Enable();
    }

    public void FixedUpdate()
    {
        HandleMovement();

        if (_inputRotateDirection != 0)
        {
            HandleRotate();
        }

        if (_isShooting)
        {
            HandleShoot();
        }
    }

    private void HandleRotate()
    {
        _objectRotate.Rotate(_inputRotateDirection);
    }

    private void HandleMovement()
    {
        _objectMovement.Move(_inputMovementDirection);
    }

    private void HandleShoot()
    {
        _objectShoot.Shoot();
        _isShooting = false;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputMovementDirection = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            _inputMovementDirection = 0f;
        }
    }

    public void OnRotate(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _inputRotateDirection = context.ReadValue<float>();
        }
        else if (context.canceled)
        {
            _inputRotateDirection = 0f;
        }
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        _isShooting = true;
    }

    private void OnDisable()
    {
        _moveAction?.Disable();
        _rotateAction?.Disable();
        _shootAction?.Disable();
    }

    private void OnDestroy()
    {
        _moveAction.performed -= OnMove;
        _moveAction.canceled -= OnMove;

        _rotateAction.performed -= OnRotate;
        _rotateAction.canceled -= OnRotate;

        _shootAction.started -= OnShoot;
    }
}
