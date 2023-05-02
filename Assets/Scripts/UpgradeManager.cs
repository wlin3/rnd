using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    HealthBar healthBar;
    BasicAbilityE abilityScript;
    public GameObject airSniperPrefab;

    private List<int> appliedUpgrades; // list to keep track of applied upgrade IDs

    private void Awake()
    {
        LoadAppliedUpgrades(); // load applied upgrades from file during game start-up
    }

    public void Update()
{
    playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();
    abilityScript = GameObject.Find("SwordParent").GetComponent<BasicAbilityE>();

    // Check if the health bar exists in the scene
    GameObject healthBarObj = GameObject.Find("Main Gui");
    if (healthBarObj != null)
    {
        healthBar = healthBarObj.GetComponentInChildren<HealthBar>();
    }
    else
    {
       return;
    }
}

    public void ApplyUpgrade(int upgradeId)
    {
        switch (upgradeId)
        {
            case 1:
                AddMaxHP();
                break;
            case 2:
                TurnBowIntoTripleShot();
                break;
            case 3:
                AddNewWeapon();
                break;
            // add more cases for other upgrades
            case 999:
                JokeUpgrade();
                break;
            case 1000:
                AirSniper();
                break;
            default:
                Debug.LogError("Invalid upgrade ID");
                break;
        }
        appliedUpgrades.Add(upgradeId);
        SaveAppliedUpgrades();
    }
    private void LoadAppliedUpgrades()
    {
        // Load the list of applied upgrade IDs from a binary file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/appliedUpgrades.dat", FileMode.OpenOrCreate);
        try
        {
            appliedUpgrades = (List<int>)formatter.Deserialize(file);
        }
        catch (System.Exception)
        {
            appliedUpgrades = new List<int>();
        }
        file.Close();
        Debug.Log("Attempted To Load Data");
        DebugPrintAppliedUpgrades();
        
        // Apply each of the loaded upgrades
        foreach (int upgradeId in appliedUpgrades)
        {
            ApplyUpgrade(upgradeId);
        }
    }


    private void SaveAppliedUpgrades()
    {
        // Save the list of applied upgrade IDs to a binary file
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/appliedUpgrades.dat");
        formatter.Serialize(file, appliedUpgrades);
        file.Close();
        Debug.Log("Attempted To Save Data");
        DebugPrintAppliedUpgrades();
    }



    private void AddMaxHP()
    {
        playerHealth.maxHealth += 15;
        healthBar.SetMaxHealth(playerHealth.maxHealth);
        playerHealth.TakeDamage(-15);
    }

    private void TurnBowIntoTripleShot()
    {
        // implement logic to turn bow into triple shot here
    }

    private void AddNewWeapon()
    {
    }

    private void JokeUpgrade()
    {
        abilityScript.shotgunBulletCount += 100;
    }

    private void AirSniper()
    {
        abilityScript.shotgunBulletPrefab = airSniperPrefab;
    }

    private void DebugPrintAppliedUpgrades()
{
    string upgradesStr = "";
    foreach (int upgradeId in appliedUpgrades)
    {
        upgradesStr += upgradeId + ",";
    }
    if (upgradesStr.Length > 0)
    {
        upgradesStr = upgradesStr.Substring(0, upgradesStr.Length - 1);
    }
    Debug.Log("Saved Upgrades: " + upgradesStr);
}

}
