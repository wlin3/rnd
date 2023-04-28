using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAbilityE : MonoBehaviour
{
    public GameObject tapBulletPrefab;
    public GameObject chargeBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public Transform shootPoint;
    public int tapBulletCount = 1;
    public int chargeBulletCount = 2;
    public int shotgunBulletCount = 5;
    public float shotgunSpreadAngle = 30f;
    public float abilityCooldown = 2f;

    private bool isCharging = false;
    private float chargeStartTime;
    private int chargeLevel = 0;

    public CooldownSystem cooldownSystem;

    void Awake()
    {
        cooldownSystem = CooldownSystem.instance;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            isCharging = true;
            chargeStartTime = Time.time;
            chargeLevel = 0;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            if (isCharging)
            {
                Shoot();
            }
            isCharging = false;
            chargeLevel = 0;
        }
        if (isCharging)
        {
            float chargeTime = Time.time - chargeStartTime;
            if (chargeTime >= 3f)
            {
                chargeLevel = 3;
            }
            else if (chargeTime >= 2f)
            {
                chargeLevel = 2;
            }
            else if (chargeTime >= 1f)
            {
                chargeLevel = 1;
            }
            else
            {
                chargeLevel = 0;
            }
        }
    }

    void Shoot()
    {
        switch (chargeLevel)
        {
            case 0:
                for (int i = 0; i < tapBulletCount; i++)
                {
                    Instantiate(tapBulletPrefab, GetMouseWorldPosition(), Quaternion.identity);
                }
                break;
            case 1:
                for (int i = 0; i < chargeBulletCount; i++)
                {
                    Instantiate(chargeBulletPrefab, GetMouseWorldPosition(), Quaternion.identity);
                }
                break;
            case 2:
                Instantiate(shotgunBulletPrefab, GetMouseWorldPosition(), Quaternion.identity);
                Vector3 mouseDirection = GetMouseWorldPosition() - shootPoint.position;
                for (int i = 0; i < shotgunBulletCount - 1; i++)
                {
                    Quaternion spreadRotation;
                    if (i % 2 == 0)
                    {
                        spreadRotation = Quaternion.Euler(Random.Range(0f, shotgunSpreadAngle),
                        Random.Range(-shotgunSpreadAngle, 0f), 0f);
                    }
                    else
                    {
                        spreadRotation = Quaternion.Euler(Random.Range(0f, shotgunSpreadAngle),
                        Random.Range(0f, shotgunSpreadAngle),
                        0f);
                    }
                    Instantiate(shotgunBulletPrefab, GetMouseWorldPosition(), spreadRotation);
                }
                break;
        }
        cooldownSystem.Cooldown3(abilityCooldown);
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePos);
    }
}
