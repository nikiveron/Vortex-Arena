using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class ObjectHealth : MonoBehaviour
{
    [SerializeField] private float _maxState = 100f;
    [SerializeField] private UnityEvent<float, float> _onStateChanged;
    [SerializeField] private UnityEvent<float> _onDamaged;
    [SerializeField] private UnityEvent _onDestroyed;

    private float _currentState = 100f;
    public float MaxState => _maxState;
    public float CurrentState => _currentState;
    public bool IsAlive => _currentState > 0;

    private void Awake()
    {
        Initialize();
    }

    public void Initialize()
    {
        SetCurrentState(_maxState);
    }

    public void Restore(float amount)
    {
        if (IsAlive)
        {
            float restoreAmount = Mathf.Abs(amount);
            ChangeState(restoreAmount);
        }
    }

    public void KillInstantly()
    {
        if (IsAlive)
        {
            SetCurrentState(0);
        }
    }

    public void TakeDamage(float damageAmount)
    {
        if (IsAlive)
        {
            float damage = Mathf.Abs(damageAmount) * -1;
            _onDamaged.Invoke(damage);  
            ChangeState(damage);
        }
    }

    private void ChangeState(float amount)
    {
        float newState = _currentState + amount;
        if (_currentState != newState)
        {
            SetCurrentState(newState);
        }
    }

    private void SetCurrentState(float state)
    {
        _currentState = Mathf.Clamp(state, 0, _maxState);
        _onStateChanged.Invoke(_maxState, _currentState);

        if (!IsAlive)
        {
            _onDestroyed.Invoke();
        }
    }
}
