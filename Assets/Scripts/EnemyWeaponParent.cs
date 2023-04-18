using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponParent : MonoBehaviour
{
    public Transform weaponTransform;
    private Transform playerTransform;
    public Transform projectileSpawnPoint;
    private EnemyMovement enemyMovement;

    public GameObject chaserWeaponSprite;
    public GameObject shooterWeaponSprite;
    public GameObject shotgunnerWeaponSprite;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyMovement = GetComponentInParent<EnemyMovement>();
    }

    void Update()
{

    Vector3 direction = playerTransform.position - transform.position;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

}
}
