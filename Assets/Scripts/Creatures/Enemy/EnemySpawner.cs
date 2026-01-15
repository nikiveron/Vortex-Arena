using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private float _minSpawnInterval = 3f;
    [SerializeField] private float _maxSpawnInterval = 7f;

    private void OnEnable()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator SpawnCoroutine()
    {
        while (true)
        {
            SpawnOnce();
            float delay = Random.Range(_minSpawnInterval, _maxSpawnInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnOnce()
    {
        foreach (Transform spawnPoint in _spawnPoints)
        {
            Vector3 spawnPosition = spawnPoint.position;
            Quaternion spawnDirection = spawnPoint.rotation;
            Instantiate(_enemyPrefab, spawnPosition, spawnDirection);
        }
    }
}
