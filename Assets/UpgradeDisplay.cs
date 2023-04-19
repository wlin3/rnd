using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;

public class UpgradeDisplay : MonoBehaviour
{

    public UpgradeCard upgradeCard; 

    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public Image artworkImage;

    public TMP_Text costText;

    public int upgradeID;


    // Start is called before the first frame update 
    public void Start()
    {
        nameText.text = upgradeCard.upgradeName;
        descriptionText.text = upgradeCard.description;
        artworkImage.sprite = upgradeCard.artwork;
        costText.text = upgradeCard.cost.ToString();
        upgradeID = upgradeCard.upgradeID;
    }

    public void InitializeUpgradeDisplay(UpgradeCard upgradeCard)
    {
        nameText.text = upgradeCard.upgradeName;
        descriptionText.text = upgradeCard.description;
        artworkImage.sprite = upgradeCard.artwork;
        costText.text = upgradeCard.cost.ToString();
        upgradeID = upgradeCard.upgradeID;
    }

    public void OnClick()
    {
        Debug.Log(upgradeCard.upgradeName + " with id " + upgradeID + " was pressed");
    }


}
