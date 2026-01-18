using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDeathHandler : MonoBehaviour
{
    [SerializeField] protected Renderer[] _renderers;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Collider[] _colliders;

    public virtual void HandleDestroyed()
    {
        DisableRenderer();
        DisablePhysics();
        Destroy(gameObject);
    }

    private void DisableRenderer()
    {
        foreach (var renderer in _renderers)
        {
            renderer.enabled = false;
        }
    }

    private void DisablePhysics()
    {
        _rigidbody.isKinematic = true;
        _rigidbody.detectCollisions = false;
        foreach (var collider in _colliders)
        {
            collider.enabled = false;
        }
    }
}
