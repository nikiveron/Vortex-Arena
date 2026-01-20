using System;
using UnityEngine;

public abstract class ObjectMovement : MonoBehaviour
{
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField, Range(0f, 100f)] protected float _defaultAcceleration = 50f;
    [SerializeField, Range(0f, 100f)] protected float _maxSpeed = 6f;
}