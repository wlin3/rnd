using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

[Serializable] // Mark the class as serializable
public class GameData
{
    public int testPoints;
    public bool canTeleport;
    // Add any other variables or upgrades you want to save/load
}

public class GameManager : MonoBehaviour
{
    // Singleton instance
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

    public int testPoints = 0;

    public bool canTeleport = true;

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

        // Load saved data
        LoadData();
    }

    // Save data to binary file
    private void SaveData()
    {
        // Create a new instance of GameData and set its values
        GameData data = new GameData();
        data.testPoints = testPoints;
        data.canTeleport = canTeleport;
        // Add any other variables or upgrades you want to save

        // Serialize the data to binary format
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gameData.dat"); // Change the filename and path to your preference
        formatter.Serialize(file, data);
        file.Close();
    }

    // Load data from binary file
    private void LoadData()
    {
        // Check if the save file exists
        if (File.Exists(Application.persistentDataPath + "/gameData.dat")) // Change the filename and path to your preference
        {
            // Deserialize the data from binary format
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gameData.dat", FileMode.Open); // Change the filename and path to your preference
            GameData data = (GameData)formatter.Deserialize(file);
            file.Close();

            // Set the values from the loaded data
            testPoints = data.testPoints;
            canTeleport = data.canTeleport;
            // Set any other variables or upgrades from the loaded data
        }
    }

    public void AddPoints(int points)
    {
        testPoints += points;
        Debug.Log("You have " + testPoints + " points");
        // Call save data after modifying data
        SaveData();
    }

    public void WinEnemyStage()
    {
        if (canTeleport)
        {
            Debug.Log("You beat stage");
            SceneManager.LoadScene("Main Scene");
        }

        else
        {
            Debug.Log("You beat stage");
        }
    }

    public void DeleteSave()
    {
        // Delete the save data file from the persistent data path
        if (File.Exists(Application.persistentDataPath + "/gameData.dat")) // Change the filename and path to your preference
        {
            File.Delete(Application.persistentDataPath + "/gameData.dat");
            Debug.Log("Save data deleted");

            testPoints = 0;
            canTeleport = true;
        }
        else
        {
            Debug.Log("No save data found");
        }
    }
}