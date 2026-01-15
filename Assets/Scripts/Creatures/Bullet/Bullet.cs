using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Bullet : MonoBehaviour
{
    public GameObject Shooter { get; private set; }
    public CreatureType ShooterType { get; private set; }
    [SerializeField] private UnityEvent _onInitialized;

    public void Initialize(GameObject shooter, CreatureType creatureType)
    {
        Shooter = shooter;
        ShooterType = creatureType;
        _onInitialized.Invoke();
    }
}
