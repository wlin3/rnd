using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int minEnemies = 15;
    public int maxEnemies = 45;
    public float spawnRadius = 10f;
    public float spawnDelay = 0.5f;
    private int numberOfEnemies;

    private int enemiesToSpawn;
    private int enemiesSpawned;
    private int maxEnemiesOnScreen;
    public GameObject player;

    private int enemyCount = 0;

    private void Start()
    {
        numberOfEnemies = Random.Range(minEnemies, maxEnemies + 1);

        // Calculate max number of enemies on screen based on maxEnemies
        maxEnemiesOnScreen = Mathf.CeilToInt(numberOfEnemies / 3f);

        // Calculate number of enemies to spawn in first frame
        enemiesToSpawn = Mathf.FloorToInt(numberOfEnemies / 5f);

        // Spawn initial enemies
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        // Check if any new enemies need to be spawned
        if (enemiesSpawned < maxEnemies && transform.childCount < maxEnemiesOnScreen)
        {
            SpawnEnemy();
        }

        // Debug log number of enemies on screen and number of enemies left to spawn
        //Debug.Log("Enemies on screen: " + transform.childCount + ", Enemies left to spawn: " + (maxEnemies - enemiesSpawned));
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= maxEnemies)
        {
            return;
        }

        Vector3 spawnPosition = GetRandomSpawnPosition();
        // Set the y and z values to 0, and the x value to the spawn radius
        //spawnPosition = new Vector3(spawnPoint, 0f, 0f);

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, transform);

        // Add a null check for newEnemy
        if (newEnemy == null)
        {
            return;
        }

        // Rename the enemy GameObject
        enemyCount++;
        newEnemy.name = "Enemy" + enemyCount;

        enemiesSpawned++;

        // You can add any additional code here that needs to access the new enemy GameObject
    }
    private Vector3 GetRandomSpawnPosition()
    {
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        Vector3 spawnPoint = player.transform.position + new Vector3(randomDirection.x, 0f, 0f) * spawnRadius;
        return spawnPoint;
    }

    private void OnValidate()
    {
        // Ensure that minEnemies and maxEnemies are within valid ranges
        minEnemies = Mathf.Clamp(minEnemies, 1, int.MaxValue);
        maxEnemies = Mathf.Clamp(maxEnemies, minEnemies, int.MaxValue);
    }
}