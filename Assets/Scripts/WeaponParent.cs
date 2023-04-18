using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class WeaponParent : MonoBehaviour
{
    private Transform spriteTransform;
    public GameObject projectile;
    public bool canAttack;
    public float timer;
    public float timeBetweenFiring;
    public float offSet;
    public Transform projectileSpawnPoint;
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

        if (!canAttack)
        {
            timer += Time.deltaTime;
            if (timer > timeBetweenFiring)
            {
                canAttack = true;
                timer = 0;
            }
        }

        if (Input.GetMouseButton(0) && canAttack)
        {
            SlashAttack();
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
}
