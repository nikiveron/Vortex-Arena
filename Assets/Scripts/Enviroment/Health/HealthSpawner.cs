using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSpawner : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _healthItemPrefab;
    [SerializeField] private Vector3 _mapCenter = Vector3.zero;
    [SerializeField] private Vector2 _mapRadius = new(65f, 65f);
    [SerializeField] private float _maxSpawnedItems = 10f;
    [SerializeField] private float _startCoolDown = 30f;
    [SerializeField] private float _minSpawnInterval = 40f;
    [SerializeField] private float _maxSpawnInterval = 90f;

    private int _currentItemsAmount = 0;

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
        yield return new WaitForSeconds(_startCoolDown);
        while (true)
        {
            if (_currentItemsAmount < _maxSpawnedItems)
            SpawnOnce();
            float delay = Random.Range(_minSpawnInterval, _maxSpawnInterval);
            yield return new WaitForSeconds(delay);
        }
    }

    private void SpawnOnce()
    {
        Vector3 spawnPosition = new(Random.Range(_mapRadius.x * -1, _mapRadius.x), 0, Random.Range(_mapRadius.y * -1, _mapRadius.y));
        spawnPosition.y = _player.position.y;
        Quaternion spawnDirection = new();
        GameObject item = Instantiate(_healthItemPrefab, spawnPosition, spawnDirection);
        _currentItemsAmount++;
    }
}
