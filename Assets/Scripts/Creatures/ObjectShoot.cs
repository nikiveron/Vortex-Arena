using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class ObjectShoot : MonoBehaviour
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

    public bool TryShoot()
    {
        if (IsAvaliable)
        {
            _currentState--;
            _onStateChanged.Invoke(_maxState, _currentState);
            return true;
        }
        return false;
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