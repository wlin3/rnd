using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwap : MonoBehaviour
{
    public List<WeaponParent> weapons;
    public Dictionary<KeyCode, WeaponParent> keyMap;
    // Start is called before the first frame update
    void Start()
    {
        keyMap = new Dictionary<KeyCode, WeaponParent>();
        for (int i = 0; i < weapons.Count; i++)
        {
            KeyCode keyCode = (KeyCode)((int)KeyCode.Alpha1 + i);
            keyMap[keyCode] = weapons[i];
        }

        // Enable the first weapon by default
        if (weapons.Count > 0)
        {
            weapons[0].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for key presses and switch weapons if necessary
        foreach (KeyValuePair<KeyCode, WeaponParent> pair in keyMap)
        {
            if (Input.GetKeyDown(pair.Key))
            {
                SwitchWeapon(pair.Value);
            }
        }
    }

    void SwitchWeapon(WeaponParent newWeapon)
    {
        foreach (WeaponParent weapon in weapons)
        {
            if (weapon == newWeapon)
            {
                weapon.gameObject.SetActive(true);
            }
            else
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }
}
