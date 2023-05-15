using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoGUI : MonoBehaviour
{
    public static DemoGUI instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void HideGUi()
    {
        Debug.Log("Tried to hide Demo GUI");
        gameObject.SetActive(false);
    }

    public void ShowGui()
    {
        gameObject.SetActive(true);
        Debug.Log("Tried to show Demo Gui");
    }
}
