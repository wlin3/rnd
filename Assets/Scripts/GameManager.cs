using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int testPoints = 0;

    // Name of the GameManager GameObject
    private string mainGameManagerObjectName = "[Main] Game Manager"; // New name for the main GameManager object

    private void Awake()
    {
        // Check if another GameManager instance exists
        GameObject[] gameManagers = GameObject.FindGameObjectsWithTag("GameManager");
        if (gameManagers.Length > 1)
        {
            Debug.Log("Successfully destroyed any clones");
            // Destroy this instance if another instance exists
            Destroy(gameObject);
            return;
        }
        else if (gameManagers.Length == 1 && gameManagers[0] != gameObject)
        {
            // Rename and reassign the existing GameManager instance
            instance = gameManagers[0].GetComponent<GameManager>();
            
            Destroy(gameObject);
            return;
        }
        Debug.Log("Successfully created the GameManager");
        gameManagers[0].name = mainGameManagerObjectName;
        // Set up the singleton instance
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void AddPoints(int points)
    {
        testPoints += points;
        Debug.Log("You have " + testPoints + " points");
    }
    
    public void WinEnemyStage()
    {
        Debug.Log("You beat stage");
        SceneManager.LoadScene("Main Scene");
    }
}
