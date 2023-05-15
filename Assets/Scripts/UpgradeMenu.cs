using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public static UpgradeMenu instance;
    public UpgradeCard[] commonUpgrades;
    public UpgradeCard[] rareUpgrades;
    public UpgradeCard[] legendaryUpgrades;

    public UpgradeDisplay[] upgradeDisplays;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        List<UpgradeCard> uniqueUpgrades = GetUniqueUpgrades();

        // Assign the upgrade scriptable objects to the UpgradeDisplays
        for (int i = 0; i < uniqueUpgrades.Count; i++)
        {
            upgradeDisplays[i].upgradeCard = uniqueUpgrades[i];
            upgradeDisplays[i].Start();
        }

        gameObject.SetActive(false);
    }

    public List<UpgradeCard> GetUniqueUpgrades()
    {
        // Create a list of all available upgrades
        List<UpgradeCard> allUpgrades = new List<UpgradeCard>();

        // Add common upgrades
        for (int i = 0; i < commonUpgrades.Length; i++)
        {
            allUpgrades.Add(commonUpgrades[i]);
        }

        // Add rare upgrades
        for (int i = 0; i < rareUpgrades.Length; i++)
        {
            allUpgrades.Add(rareUpgrades[i]);
        }

        // Add legendary upgrades
        for (int i = 0; i < legendaryUpgrades.Length; i++)
        {
            allUpgrades.Add(legendaryUpgrades[i]);
        }

        // Randomly select 3 unique upgrades with different rarities
        List<UpgradeCard> uniqueUpgrades = new List<UpgradeCard>();
        while (uniqueUpgrades.Count < 3 && allUpgrades.Count > 0)
        {
            int randomValue = Random.Range(0, 100);
            UpgradeCard randomUpgrade = null;

            if (randomValue < 60) // 60% chance of selecting a common upgrade
            {
                randomUpgrade = commonUpgrades[Random.Range(0, commonUpgrades.Length)];
            }
            else if (randomValue < 80) // 20% chance of selecting a rare upgrade
            {
                randomUpgrade = rareUpgrades[Random.Range(0, rareUpgrades.Length)];
            }
            else // 10% chance of selecting a legendary upgrade
            {
                randomUpgrade = legendaryUpgrades[Random.Range(0, legendaryUpgrades.Length)];
            }

            if (!uniqueUpgrades.Contains(randomUpgrade))
            {
                uniqueUpgrades.Add(randomUpgrade);
            }

            allUpgrades.Remove(randomUpgrade);
        }

        return uniqueUpgrades;
    }


    public void ShowUpgradeMenu()
    {
        Debug.Log("Tried to show upgrade gui");
        gameObject.SetActive(true);
        GameManager.Instance.SystemPause(true);
    }

    public void HideUpgradeMenu()
    {
        Debug.Log("Tried to hide upgrade gui");
        gameObject.SetActive(false);
        GameManager.Instance.SystemPause(false);
    }

}
