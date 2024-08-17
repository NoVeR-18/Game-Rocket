using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private Vector2 _spawnIntervalRange = new Vector2(0.5f, 2f);
    [SerializeField] private Vector2 _screenHalfSizeWorldUnits;

    private float _nextSpawnTime;

    private void Start()
    {
        _screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
        ScheduleNextSpawn();
    }

    private void Update()
    {
        if (Time.time >= _nextSpawnTime)
        {
            SpawnEnemy();
            ScheduleNextSpawn();
        }
    }

    private void SpawnEnemy()
    {
        Vector2 spawnPosition = new Vector2(Random.Range(-_screenHalfSizeWorldUnits.x, _screenHalfSizeWorldUnits.x), _screenHalfSizeWorldUnits.y);
        var enemy = Instantiate(_enemyPrefab[Random.Range(0, _enemyPrefab.Length)], spawnPosition, Quaternion.identity);
        GameManager.Instance._miniGameMenu._createdObjects.Add(enemy);
    }

    private void ScheduleNextSpawn()
    {
        _nextSpawnTime = Time.time + Random.Range(_spawnIntervalRange.x, _spawnIntervalRange.y);
    }
}
