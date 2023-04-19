using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeSlot : MonoBehaviour
{
    public Image upgradeImage;
    public TMP_Text upgradeName;
    public TMP_Text upgradeDescription;
    public TMP_Text upgradeCost;

    public void SetUpgrade(string name, Sprite sprite, string description, int cost)
    {
        upgradeName.text = name;
        upgradeImage.sprite = sprite;
        upgradeDescription.text = description;
        upgradeCost.text = cost.ToString();
    }

    public void BuyUpgrade()
    {
        int cost = int.Parse(upgradeCost.text);

        if (GameManager.Instance.upgradePoints >= cost)
        {
            GameManager.Instance.upgradePoints -= cost;
            ApplyUpgradeEffects();
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("You don't have enough upgrade points to purchase this upgrade.");
        }
    }

    public void ApplyUpgradeEffects()
    {
        // TODO: Apply the effects of the upgrade to the player
    }
}
