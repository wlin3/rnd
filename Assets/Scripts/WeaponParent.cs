using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private Transform spriteTransform;
    

    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - spriteTransform.position.x, mousePosition.y - spriteTransform.position.y);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
