using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraFollow : MonoBehaviour
{
    private const float _startHorizontalAngle = 0f;
    private const float _startVerticalAngle = 0f;

    [SerializeField] private Transform _playerTransform;
    [SerializeField, Range(0f, 20f)] private float _radius = 5f;
    [SerializeField, Range(0f, 89f)] private float _angle = 60f;
    
    private void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(_angle, 0, 0f);
        Vector3 direction = rotation * Vector3.forward;
        Vector3 offset = direction * _radius;
        Vector3 position = _playerTransform.position - offset;

        transform.position = position;
        transform.rotation = rotation;  
    }
}
