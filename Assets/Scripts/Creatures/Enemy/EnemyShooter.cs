using System.Collections;
using UnityEngine;

public class EnemyShooter : MonoBehaviour
{
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _minShootInterval = 3f;
    [SerializeField] private float _maxShootInterval = 7f;
    [SerializeField] private float _checkPlayerRadius = 2f;
    [SerializeField] private LayerMask _checkPlayerLayer;
    private bool _canShoot = false;

    private void FixedUpdate()
    {
        CheckPlayerAround();
    }

    private void OnEnable()
    {
        StartCoroutine(ShootCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Shoot(bool canShoot)
    {
        _canShoot = canShoot;
    }


    private IEnumerator ShootCoroutine()
    {
        while (true)
        {
            if (_canShoot)
                ShootOnce();

            float delay = Random.Range(_minShootInterval, _maxShootInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    private void ShootOnce()
    {
        foreach (Transform firePoint in _firePoints)
        {
            Vector3 spawnPosition = firePoint.position;
            Quaternion bulletDirection = firePoint.rotation;
            Instantiate(_bulletPrefab, spawnPosition, bulletDirection);
        }
    }

    private void CheckPlayerAround()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _checkPlayerRadius, _checkPlayerLayer);

        foreach (Collider hit in hits)
        {
            if (hit.TryGetComponent<PlayerController>(out var playerController))
            {
                //activator.TryActivate();
                return;
            }
        }
    }
}
