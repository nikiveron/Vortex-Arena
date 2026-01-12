
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VelocityBoost : MonoBehaviour
{
    [SerializeField] private float _maxState = 100f;
    private float _currentState = 100f;
    public float MaxState => _maxState;
    public float CurrentState => _currentState;
    public bool IsAvaliable => _currentState > 0;

    private void Start()
    {
        StartCoroutine(AddStamina());
    }

    public bool TryBoost()
    {
        if (IsAvaliable)
        {
            _currentState--;
            return true;
        }
        return false;
    }

    public IEnumerator AddStamina()
    {
        while (true)
        {
            if (_currentState < _maxState)
            {
                _currentState++;
            }
            yield return new WaitForSeconds(0.2f);
        }
    }
}