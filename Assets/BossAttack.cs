using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    public float projectileSpeed = 10f;
    public float fireRate = 1f;
    private float nextFireTime;
    private Transform playerTransform;

    // Define the variables here
    private float shooterAttackCooldownTimer;
    private float shooterAttackCooldownMin = 1f;
    private float shooterAttackCooldownMax = 2f;
    private GameObject shooterProjectilePrefab;
    private Vector2 directionToPlayer;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get a reference to the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        shooterProjectilePrefab = Resources.Load<GameObject>("Fireball");
        // Initialize the variables here
        shooterAttackCooldownTimer = Random.Range(shooterAttackCooldownMin, shooterAttackCooldownMax);
        directionToPlayer = (playerTransform.position - animator.transform.position).normalized;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        ShootAtTarget(playerTransform.position, animator);
        // Reset attack cooldown timer

    }

    void ShootAtTarget(Vector2 targetPosition, Animator animator)
    {
        // Create projectile and set its position and rotation
        GameObject newProjectile = Instantiate(shooterProjectilePrefab, animator.transform.position, Quaternion.identity);
        newProjectile.transform.right = targetPosition - (Vector2)animator.transform.position;

        // Add force to the projectile in the direction of the player
        Vector2 direction = (targetPosition - (Vector2)animator.transform.position).normalized;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
    }
}
