using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private Transform spriteTransform;
    public GameObject projectile;
    public GameObject chargedProjectile;
    public bool canAttack;
    public float timer;
    public float timeBetweenFiring;
    public float chargedAttackCooldown;
    public float offSet;
    public Transform projectileSpawnPoint;
    public bool isMelee;
    public bool chargedAttack;

    private CooldownSystem cooldownSystem;

    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform;
        cooldownSystem = CooldownSystem.instance;
        if (isMelee)
        {
            cooldownSystem.ChargedCooldown(chargedAttackCooldown);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - spriteTransform.position.x, mousePosition.y - spriteTransform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        

        if (Input.GetMouseButton(0) && cooldownSystem.basicCanAttack && !GameManager.Instance.isPaused && !cooldownSystem.chargedCanAttack)
        {
            SlashAttack();
            cooldownSystem.BasicCooldown(timeBetweenFiring);
            cooldownSystem.ChargedCooldown(chargedAttackCooldown);
        }

        if (Input.GetMouseButton(0) && cooldownSystem.chargedCanAttack && !GameManager.Instance.isPaused)
        {
            ChargedAttack();
            cooldownSystem.ChargedCooldown(chargedAttackCooldown);
        }
    }

    void SlashAttack()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from weapon to mouse
        Vector3 direction = mousePosition - spriteTransform.position;

        // Calculate offset based on weapon rotation
        Vector3 spawnOffset = transform.right * offSet;
        
        canAttack = false;
        Instantiate(projectile, projectileSpawnPoint.position + spawnOffset, Quaternion.identity);
    }

    void ChargedAttack()
    {
        // Get mouse position in world space
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // Calculate direction from weapon to mouse
        Vector3 direction = mousePosition - spriteTransform.position;

        // Calculate offset based on weapon rotation
        Vector3 spawnOffset = transform.right * offSet;
        
        canAttack = false;
        Instantiate(chargedProjectile, projectileSpawnPoint.position + spawnOffset, Quaternion.identity);
    }
}
