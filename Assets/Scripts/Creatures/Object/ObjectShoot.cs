using UnityEngine;

public class ObjectShoot : MonoBehaviour
{
    [SerializeField] protected CreatureType _creatureType;
    [SerializeField] protected Transform[] _firePoints;
    [SerializeField] protected GameObject _bulletPrefab;

    protected void ShootOnce()
    {
        foreach (Transform firePoint in _firePoints)
        {
            firePoint.GetPositionAndRotation(out Vector3 spawnPosition, out Quaternion bulletDirection);
            Instantiate(_bulletPrefab, spawnPosition, bulletDirection);
        }
    }
}