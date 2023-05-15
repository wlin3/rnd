using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicAbilityE : MonoBehaviour
{
    private Transform spriteTransform;
    public GameObject tapBulletPrefab;
    public GameObject chargeBulletPrefab;
    public GameObject shotgunBulletPrefab;
    public Transform shootPoint;
    public int tapBulletCount = 1;
    public int chargeBulletCount = 2;
    public int sniperBulletCount =1;
    public int shotgunBulletCount = 5;
    public float shotgunSpreadAngle = 30f;
    public float abilityCooldown = 2f;
    public float chargeCooldown = 1f;
    private float savedCooldown;
    private bool isCharging = false;
    private float chargeStartTime;
    private int chargeLevel = 0;

    private CooldownSystem cooldownSystem;
    public ChargeEffect chargeEffect;

    void Start()
    {
        spriteTransform = transform;
        cooldownSystem = CooldownSystem.instance;
        savedCooldown = abilityCooldown;

    }
    void Update()
    {
        if (Input.GetMouseButtonDown(1) && cooldownSystem.secondCanAttack)
        {   
            abilityCooldown = savedCooldown;
            isCharging = true;
            chargeStartTime = Time.time;
            chargeLevel = 0;
            
            chargeEffect.gameObject.SetActive(true);
            chargeEffect.chargeLevel = -10;
            chargeEffect.PlayAnimation();
            chargeEffect.lastChargeLevel = -9;
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (isCharging && cooldownSystem.secondCanAttack)
            {
                if(chargeLevel == 0f)
                {
                    abilityCooldown *= 0.5f;
                }
                if(chargeLevel == 1f)
                {
                    abilityCooldown *= 0.65f;
                }
                if(chargeLevel == 2f)
                {
                    abilityCooldown *= 0.85f;
                }
                Vector3 mousePosition = GetMouseWorldPosition().normalized;
                StartCoroutine(Shoot(mousePosition));

                
            }
            isCharging = false;
            chargeLevel = 0;
        }
        if (isCharging)
        {
            float chargeTime = Time.time - chargeStartTime;
            if (chargeTime >= 3 * chargeCooldown)
            {
                chargeLevel = 3;
                if(chargeEffect.lastChargeLevel != chargeLevel)
                {
                    chargeEffect.gameObject.SetActive(true);
                    chargeEffect.chargeLevel = chargeLevel;
                    chargeEffect.PlayAnimation();
                    Debug.Log(chargeLevel);
                }
                
            }
            else if (chargeTime >= 2 * chargeCooldown)
            {
                chargeLevel = 2;
                if(chargeEffect.lastChargeLevel != chargeLevel)
                {
                    chargeEffect.gameObject.SetActive(true);
                    chargeEffect.chargeLevel = chargeLevel;
                    chargeEffect.PlayAnimation();
                    Debug.Log(chargeLevel);
                }
                
            }
            else if (chargeTime >= chargeCooldown)
            {
                chargeLevel = 1;
               if(chargeEffect.lastChargeLevel != chargeLevel)
               {
                    chargeEffect.gameObject.SetActive(true);
                    chargeEffect.chargeLevel = chargeLevel;
                    chargeEffect.PlayAnimation();
                    Debug.Log(chargeLevel);
               }
                
            }
            else
            {
                chargeLevel = 0;

            }
        }
    }


    IEnumerator Shoot(Vector3 mousePosition)
    {
        switch (chargeLevel)
        {
            case 0:
                for (int i = 0; i < tapBulletCount; i++)
                {
                    // Calculate direction from weapon to mouse and normalize it
                    Vector3 direction = (mousePosition - spriteTransform.position).normalized;


                    // Calculate offset based on weapon rotation
                    Vector3 spawnOffset = transform.right;

                    Instantiate(tapBulletPrefab, shootPoint.position + spawnOffset, Quaternion.identity);
                }
                break;
            case 1:
                for (int i = 0; i < chargeBulletCount; i++)
                {
                    // Calculate direction from weapon to mouse and normalize it
                    Vector3 direction = (mousePosition - spriteTransform.position).normalized;


                    // Calculate offset based on weapon rotation
                    Vector3 spawnOffset = transform.right;

                    Instantiate(tapBulletPrefab, shootPoint.position + spawnOffset, Quaternion.identity);

                    yield return new WaitForSeconds(0.3f);
                }
                break;

            case 2:
                for (int i = 0; i < sniperBulletCount; i++)
                {
                    // Calculate direction from weapon to mouse and normalize it
                    Vector3 direction = (mousePosition - spriteTransform.position).normalized;


                    // Calculate offset based on weapon rotation
                    Vector3 spawnOffset = transform.right;

                    Instantiate(chargeBulletPrefab, shootPoint.position + spawnOffset, Quaternion.identity);
                }
                break;

            case 3:
                 for (int i = 0; i < shotgunBulletCount; i++)
                {
                    // Calculate direction from weapon to mouse and normalize it
                    Vector3 direction = (mousePosition - spriteTransform.position).normalized;


                    // Calculate offset based on weapon rotation
                    Vector3 spawnOffset = transform.right;

                    Instantiate(shotgunBulletPrefab, shootPoint.position + spawnOffset, Quaternion.identity);
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
