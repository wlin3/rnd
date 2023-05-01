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
    public int shotgunBulletCount = 5;
    public float shotgunSpreadAngle = 30f;
    public float abilityCooldown = 2f;

    private bool isCharging = false;
    private float chargeStartTime;
    private int chargeLevel = 0;

    private CooldownSystem cooldownSystem;

    void Start()
    {
        spriteTransform = transform;
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
            if (isCharging && cooldownSystem.secondCanAttack)
            {
                Vector3 mousePosition = GetMouseWorldPosition();
                StartCoroutine(Shoot(mousePosition));
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
                Debug.Log(chargeLevel);
            }
            else if (chargeTime >= 2f)
            {
                chargeLevel = 2;
                Debug.Log(chargeLevel);
            }
            else if (chargeTime >= 1f)
            {
                chargeLevel = 1;
                Debug.Log(chargeLevel);
            }
            else
            {
                chargeLevel = 0;
                Debug.Log(chargeLevel);
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
                    // Calculate direction from weapon to mouse
                    Vector3 direction = mousePosition - spriteTransform.position;

                    // Calculate offset based on weapon rotation
                    Vector3 spawnOffset = transform.right;

                    Instantiate(tapBulletPrefab, shootPoint.position + spawnOffset, Quaternion.identity);
                }
                break;
            case 1:
                for (int i = 0; i < chargeBulletCount; i++)
                {
                    // Calculate direction from weapon to mouse
                    Vector3 direction = mousePosition - spriteTransform.position;

                    // Calculate offset based on weapon rotation
                    Vector3 spawnOffset = transform.right;

                    Instantiate(chargeBulletPrefab, shootPoint.position + spawnOffset, Quaternion.identity);

                    yield return new WaitForSeconds(0.3f);
                }
                break;


            case 2:
                for (int i = 0; i < shotgunBulletCount; i++)
                {
                    // Calculate direction from weapon to mouse
                    Vector3 direction = mousePosition - spriteTransform.position;

                    // Calculate the spread angle for this bullet
                    float spreadAngle = shotgunSpreadAngle * ((float)i / (float)(shotgunBulletCount - 1)) - (shotgunSpreadAngle / 2f);

                    // Calculate the spread direction
                    Quaternion spreadRotation = Quaternion.AngleAxis(spreadAngle, Vector3.forward);
                    Vector3 spreadDirection = spreadRotation * direction;

                    // Instantiate the bullet
                    Instantiate(shotgunBulletPrefab, shootPoint.position + (spreadDirection.normalized * 0.5f), Quaternion.LookRotation(spreadDirection, Vector3.forward));


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
