using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerShoot : ObjectShoot
{
    [SerializeField] private float _maxState = 100f;
    [SerializeField] private float _additionDelay = 5f;
    [SerializeField] private UnityEvent<float, float> _onStateChanged;

    private float _currentState = 100f;
    public float MaxState => _maxState;
    public float CurrentState => _currentState;
    public bool IsAvaliable => _currentState > 0;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        _onStateChanged.Invoke(_maxState, _currentState);
    }

    private void Start()
    {
        StartCoroutine(AddShoot());
    }

    public void Shoot()
    {
        if (IsAvaliable)
        {
            ShootOnce();
            _currentState--;
            _onStateChanged.Invoke(_maxState, _currentState);
        }
    }

    public IEnumerator AddShoot()
    {
        while (true)
        {
            if (_currentState < _maxState)
            {
                _currentState++;
                _onStateChanged.Invoke(_maxState, _currentState);
            }
            yield return new WaitForSeconds(_additionDelay);
        }
    }
}