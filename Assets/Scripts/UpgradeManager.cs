using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    PlayerHealth playerHealth;
    HealthBar healthBar;
    public void Update()
{
    playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

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
            default:
                Debug.LogError("Invalid upgrade ID");
                break;
        }
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
}
