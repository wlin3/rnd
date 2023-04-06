using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int minEnemies = 10;
    public int maxEnemies = 45;
    public float spawnRadius = 10f;
    public float spawnDelay = 0.5f;
    public float maxEnemiesOnScreen = 15f;

    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private Camera mainCamera;
    private float timeSinceLastSpawn;

    void Start()
    {
        mainCamera = Camera.main;
        SpawnEnemies();
    }

    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= spawnDelay && spawnedEnemies.Count < maxEnemies)
        {
            SpawnEnemies();
            timeSinceLastSpawn = 0f; // reset timeSinceLastSpawn
        }
    }

    private void SpawnEnemies()
    {
        int enemiesToSpawn = Random.Range(minEnemies, maxEnemies + 1);
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemiesToSpawn && spawnedEnemies.Count < maxEnemies)
        {
            GameObject enemyObject = Instantiate(enemyPrefab, GetRandomSpawnPoint(), Quaternion.identity);
            spawnedEnemies.Add(enemyObject);
            enemiesSpawned++;

            if (spawnedEnemies.Count >= Mathf.FloorToInt(maxEnemies / 3f))
            {
                break;
            }
        }
    }


    private Vector3 GetRandomSpawnPoint()
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPoint = transform.position + new Vector3(randomPoint.x, 0f, randomPoint.y);
        return spawnPoint;
    }
}
