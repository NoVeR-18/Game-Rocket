using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _asteroidPrefab = new GameObject[6];

    private Vector2 _secondsBetweenSpawnMinMax = new Vector2(0.05f, 1f);
    private float _nextSpawnTime;
    private Vector2 _screenHalfSizeWorldUnits;
    private bool _isSpawning = false;
    public List<GameObject> _asteroids = new List<GameObject>();

    void Start()
    {
        _screenHalfSizeWorldUnits = new Vector2(Camera.main.aspect * Camera.main.orthographicSize, Camera.main.orthographicSize);
    }


    void Update()
    {
        if (Time.time > _nextSpawnTime && _isSpawning)
        {
            AsteroidSpawn();
        }
    }

    private void AsteroidSpawn()
    {
        float secondsBetweenSpawn = Mathf.Lerp(_secondsBetweenSpawnMinMax.y, _secondsBetweenSpawnMinMax.x, 0.5f);
        _nextSpawnTime = Time.time + secondsBetweenSpawn;

        Vector2 spawnPosition = new Vector2(Random.Range(-_screenHalfSizeWorldUnits.x, _screenHalfSizeWorldUnits.x), _screenHalfSizeWorldUnits.y);
        GameObject newAsteroid = (GameObject)Instantiate(_asteroidPrefab[Random.Range(0, _asteroidPrefab.Length)], spawnPosition, Quaternion.identity);
        _asteroids.Add(newAsteroid);
    }
    public void SetSpawning(bool isActive)
    {
        _isSpawning = isActive;
        if (!isActive)
        {
            foreach (var asteroid in _asteroids)
            {
                Destroy(asteroid);
            }
        }
    }
    public void SetSpawnInterval(Vector2 minMaxInterval)
    {
        _secondsBetweenSpawnMinMax = minMaxInterval;
    }
    public Vector2 GetCurrentSpawnInterval()
    {
        return _secondsBetweenSpawnMinMax;
    }
    public void TurboClick()
    {
        StartCoroutine(GameManager.Instance._spawner.TurboActivate());
    }
    private IEnumerator TurboActivate()
    {
        var currentInterval = GetCurrentSpawnInterval();
        SetSpawnInterval(new Vector2(0.1f, 0.1f));
        yield return new WaitForSeconds(5f);
        SetSpawnInterval(currentInterval);
    }
}
