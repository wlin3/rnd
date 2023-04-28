using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireRate = 1f;
    private float nextFireTime;

    private Transform playerTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get a reference to the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        projectilePrefab = Resources.Load<GameObject>("Fireball");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        shooterAttackCooldownTimer -= Time.deltaTime;

        // If attack cooldown has elapsed, shoot at the player
        if (shooterAttackCooldownTimer <= 0)
        {
        ShootAtTarget(target.position);
        // Reset attack cooldown timer
        shooterAttackCooldownTimer = Random.Range(shooterAttackCooldownMin, shooterAttackCooldownMax);
        }
    }
        void ShootAtTarget(Vector2 targetPosition)
    {
        // Create projectile and set its position and rotation
        GameObject newProjectile = Instantiate(shooterProjectilePrefab,  animator.transform.position + directionToPlayer, Quaternion.identity);
        newProjectile.transform.right = targetPosition - (Vector2)transform.position;

        // Add force to the projectile in the direction of the player
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
    }
}
