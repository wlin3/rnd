using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrades" )]
public class UpgradeCard : ScriptableObject
{
   public string upgradeName;
   public string description;

   public Sprite artwork;

   public int cost;

   public int upgradeID;
}
