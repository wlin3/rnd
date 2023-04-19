using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGui : MonoBehaviour
{
    public static MainGui instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void HideGUi()
    {
        Debug.Log("Tried to hide Menu Gui");
        gameObject.SetActive(false);
    }

    public void ShowGui()
    {
        gameObject.SetActive(true);
        Debug.Log("Tried to show Menu Gui");
    }
}
