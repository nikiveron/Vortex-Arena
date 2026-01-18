using UnityEngine;

public class ObjectDestruction : MonoBehaviour
{
    [SerializeField] private GameObject _deathEffectPrefab;

    public void Die()
    {
        Instantiate(_deathEffectPrefab, transform.position, Quaternion.identity);
    }
}
