using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMenu : MonoBehaviour
{
    public UpgradeSlot upgradeSlotPrefab;
    public int numUpgradeSlots;
    private List<(string, Sprite, string, int)> availableUpgrades = new List<(string, Sprite, string, int)>()
    {
        ("Upgrade A", spriteA, "This upgrade does X", 10),
        ("Upgrade B", spriteB, "This upgrade does Y", 20),
        ("Upgrade C", spriteC, "This upgrade does Z", 30),
        // Add more upgrades here...
    };
    private List<UpgradeSlot> upgradeSlots = new List<UpgradeSlot>();

    void Start()
    {
        for (int i = 0; i < numUpgradeSlots; i++)
        {
            // Get a random upgrade from the available upgrades list
            (string upgradeName, Sprite upgradeSprite, string upgradeDescription, int upgradeCost) = availableUpgrades[Random.Range(0, availableUpgrades.Count)];
            availableUpgrades.Remove((upgradeName, upgradeSprite, upgradeDescription, upgradeCost));

            // Instantiate an UpgradeSlot prefab and set its upgrade information
            UpgradeSlot upgradeSlot = Instantiate(upgradeSlotPrefab, transform);
            upgradeSlot.SetUpgrade(upgradeName, upgradeSprite, upgradeDescription, upgradeCost);

            // Add the instantiated UpgradeSlot to the list of UpgradeSlots
            upgradeSlots.Add(upgradeSlot);
        }
    }

    public void ConfirmUpgrades()
    {
        foreach (UpgradeSlot upgradeSlot in upgradeSlots)
        {
            upgradeSlot.BuyUpgrade();
        }
        upgradeSlots.Clear();
    }
}
