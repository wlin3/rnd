using System.Collections.Generic;
using UnityEngine;

public class RerollButton : MonoBehaviour
{
    public UpgradeMenu upgradeMenu;

    public void OnClick()
    {
        List<UpgradeCard> uniqueUpgrades = upgradeMenu.GetUniqueUpgrades();

        // Assign the upgrade scriptable objects to the UpgradeDisplays
        for (int i = 0; i < uniqueUpgrades.Count; i++)
        {
            upgradeMenu.upgradeDisplays[i].upgradeCard = uniqueUpgrades[i];
            upgradeMenu.upgradeDisplays[i].Start();
        }
    }
}
