using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _minSpawnInterval = 3f;
    [SerializeField] private float _maxSpawnInterval = 7f;
    private int _amountOfPrefabs = 0;

    private void OnEnable()
    {
        _amountOfPrefabs = _enemyPrefabs.Length;
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
            spawnPosition.y = _player.position.y;
            Quaternion spawnDirection = spawnPoint.rotation;
            int randomEnemyId = Random.Range(0, _amountOfPrefabs);
            GameObject enemy = Instantiate(_enemyPrefabs[randomEnemyId], spawnPosition, spawnDirection);
            EnemyController enemyController = enemy.GetComponent<EnemyController>();
            enemyController.Player = _player;
        }
    }
}
