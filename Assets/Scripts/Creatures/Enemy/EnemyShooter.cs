using System.Collections;
using UnityEngine;

public class EnemyShooter : ObjectShoot
{
    [SerializeField] private float _minShootInterval = 3f;
    [SerializeField] private float _maxShootInterval = 7f;
    private bool _canShoot = false;

    private void OnEnable()
    {
        StartCoroutine(ShootCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
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

    public void Shoot(bool canShoot)
    {
        _canShoot = canShoot;
    }
    
}
