using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UpgradeDisplay : MonoBehaviour
{

    public UpgradeCard upgradeCard; 

    public TMP_Text nameText;
    public TMP_Text descriptionText;

    public Image artworkImage;

    public TMP_Text costText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = upgradeCard.upgradeName;
        descriptionText.text = upgradeCard.description;
        artworkImage.sprite = upgradeCard.artwork;
        costText.text = upgradeCard.cost.ToString();
    }

}
