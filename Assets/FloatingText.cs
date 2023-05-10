using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingText : MonoBehaviour
{   

    void Start()
    {
        //DamagePopup.Create(Vector3.zero, 300);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            DamagePopup.Create(mousePosition, 100);
        }
    }
}
