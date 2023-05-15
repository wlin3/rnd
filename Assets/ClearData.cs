using UnityEngine;
using System.IO;

public class ClearData : MonoBehaviour
{
    public void ClearSavedData()
    {
        string filePath = Application.persistentDataPath + "/appliedUpgrades.dat";
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("Cleared saved data");
        }
        else
        {
            Debug.LogWarning("No saved data to clear");
        }
    }
}
