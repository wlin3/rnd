using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponParent : MonoBehaviour
{
    private Transform spriteTransform;
    private Transform playerTransform;
    public Transform projectileSpawnPoint;

    void Start()
    {
        spriteTransform = transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Vector2 direction = (playerTransform.position - spriteTransform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        spriteTransform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

}
