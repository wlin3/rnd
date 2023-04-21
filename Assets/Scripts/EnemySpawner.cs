using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using TMPro;


public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int minEnemies = 15;
    public int maxEnemies = 45;
    public float spawnRadius = 10f;
    public float spawnDelay = 0.5f;
    private int numberOfEnemies;
    private int numberOfWins;

    private int enemiesToSpawn;
    private int enemiesSpawned;
    private int maxEnemiesOnScreen;
    public GameObject player;
    public TMP_Text enemyText;

    private int enemyCount = 0;

    private bool winCheck = false;

    private void Start()
    {
        numberOfWins = GameManager.Instance.GetWins();

        numberOfEnemies = Random.Range(minEnemies + (5 * numberOfWins), maxEnemies + 1 + (8 * numberOfWins));

        // Calculate max number of enemies on screen based on maxEnemies
        maxEnemiesOnScreen = Mathf.CeilToInt(numberOfEnemies / 3f);
        if (maxEnemiesOnScreen < 5)
        {
            maxEnemiesOnScreen = 5;
        }
        else if(maxEnemiesOnScreen > 20)
        {
            maxEnemiesOnScreen = 20;
        }
        // Calculate number of enemies to spawn in first frame
        enemiesToSpawn = Mathf.FloorToInt(numberOfEnemies / 5f);
        if(enemiesToSpawn > 20)
        {
            enemiesToSpawn = 20;
        }

        // Spawn initial enemies
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            SpawnEnemy();
        }
    }

    private void Update()
    {
        // Check if any new enemies need to be spawned
        if (enemiesSpawned < numberOfEnemies && transform.childCount < maxEnemiesOnScreen)
        {
            SpawnEnemy();
        }

        if (enemiesSpawned == numberOfEnemies && transform.childCount == 0 && !winCheck)
        {
            GameManager.Instance.WinEnemyStage();// Call the WinEnemyStage method from the GameManager script
            winCheck = true;
        }
        // Debug log number of enemies on screen and number of enemies left to spawn
        enemyText.text = "Enemies Left: " + (transform.childCount + numberOfEnemies - enemiesSpawned);
        //Debug.Log("Enemies on screen: " + transform.childCount + ", Enemies left to spawn: " + (numberOfEnemies - enemiesSpawned));
    }

    private void SpawnEnemy()
    {
        if (enemiesSpawned >= numberOfEnemies)
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
        
        // Keep generating random spawn positions until the enemy is at least 30 units away from the player
        while (Vector3.Distance(spawnPoint, player.transform.position) < 30f)
        {
            randomDirection = Random.insideUnitCircle.normalized;
            spawnPoint = player.transform.position + new Vector3(randomDirection.x, 0f, 0f) * spawnRadius;
        }

        return spawnPoint;
    }


    private void OnValidate()
    {
        // Ensure that minEnemies and maxEnemies are within valid ranges
        minEnemies = Mathf.Clamp(minEnemies, 1, int.MaxValue);
        maxEnemies = Mathf.Clamp(maxEnemies, minEnemies, int.MaxValue);
    }
}