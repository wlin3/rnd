using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathGUI : MonoBehaviour
{
    public static DeathGUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void HideGUi()
    {
        Debug.Log("Tried to hide Death GUI");
        gameObject.SetActive(false);
    }

    public void ShowGui()
    {
        gameObject.SetActive(true);
        Debug.Log("Tried to show Death Gui");
    }
}
