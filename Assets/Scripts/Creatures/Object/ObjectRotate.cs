using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotate : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField, Range(0f, 500f)] private float _rotationSpeed = 6f;

    public void Rotate(float rotateInput)
    {
        float rotation = rotateInput * _rotationSpeed * Time.fixedDeltaTime;
        _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(0, rotation, 0));
    }
}
