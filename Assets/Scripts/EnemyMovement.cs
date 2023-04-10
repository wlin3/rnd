using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovement : MonoBehaviour
{

    public enum enemyType { Chaser, Shooter, Flyer }
    public enemyType EnemyType;  // this public var should appear as a drop down
    public Transform player;
    public Transform target;
    [Header("Chaser Setup")]
    public float chaserSpeed = 5f;
    public float chaserAcceleration = 10f;
    public float chaserDeceleration = 5f;
    public float chaserMaxDistance = 99999f;
    public float chaserJumpForce = 10f;
    public float chaserJumpInterval = 2f;
    public float chaserJumpChance = 0.2f;
    private Rigidbody2D rb;
    private EnemyHealth enemyHealth;
    private float jumpTimer = 0f;
    public float chaserRetreatTime = 0.75f;
    [HideInInspector] public bool retreating = false;
    [HideInInspector] public bool canRetreat = true;
    public float maxFlingForce = 15f;
    public float chaserOvershootDistance = 10f;

    [Header("Shooter Setup")]
    public GameObject shooterProjectilePrefab;
    public float shooterMaxDistance = 10f; // max distance from the player
    public float shooterSpeed = 3f; // speed of the enemy when chasing
    public float shooterAcceleration = 10f; // acceleration of the enemy when chasing
    public float shooterAttackCooldownMin = 2f; // minimum time between attacks
    public float shooterAttackCooldownMax = 4f; // maximum time between attacks
    private float shooterAttackCooldownTimer; // timer for attack cooldown


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Transform playerPosition = player.transform.Find("Player Position");
            if (playerPosition != null)
            {
                target = playerPosition;
            }
        }
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();
        EnemyType = (enemyType)Random.Range(0, 2);
        //EnemyType = enemyType.Shooter;
        if (EnemyType == enemyType.Shooter)
        {
            enemyHealth.enemyMaxHealth = 30;
            enemyHealth.enemyCurrentHealth = enemyHealth.enemyMaxHealth;
            gameObject.name += "_Shooter";
            shooterAttackCooldownTimer = Random.Range(shooterAttackCooldownMin, shooterAttackCooldownMax);
        }
        else if(EnemyType == enemyType.Chaser)
        {
            enemyHealth.enemyMaxHealth = 75;
            enemyHealth.enemyCurrentHealth = enemyHealth.enemyMaxHealth;
            gameObject.name += "_Chaser";
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyType == enemyType.Chaser)
        {
            Chaser();
        }
        
        if (EnemyType == enemyType.Shooter)
        {
            Shooter();
        }
    }

    void Chaser()
    {

        if (retreating)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, target.position);
        
        // If the player is within range, chase the player
        if (distance <= chaserMaxDistance)
        {
            Vector2 direction = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 0);

            // Calculate the speed of the enemy based on the distance to the player
            float targetSpeed = Mathf.Clamp(chaserAcceleration * distance / chaserMaxDistance, chaserSpeed, chaserAcceleration);

            // Move towards the player
            Vector2 newVelocity = direction * targetSpeed;

            // Check if the enemy has overshot the player
            if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(direction.x) && Mathf.Abs(rb.velocity.x) > 50f)
            {
                // Calculate the overshoot direction
                Vector2 overshootDirection = (direction.x > 0) ? Vector2.right : Vector2.left;

                // Keep moving in the overshoot direction for a brief moment
                rb.velocity = Vector2.MoveTowards(rb.velocity, overshootDirection * targetSpeed, chaserDeceleration * Time.deltaTime);
            }
            else if (Mathf.Abs(target.position.x - transform.position.x) < 0.5f)
            {
                // If the enemy's x position is very close to the player's x position, keep moving in the current direction for a brief moment
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
                StartCoroutine(DecelerateAndTurnAround());
            }
            else
            {
                // Otherwise, move towards the player as normal
                rb.velocity = new Vector2(newVelocity.x, rb.velocity.y);
            }

            // Flip the enemy sprite if necessary
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Randomly jump if it's time to jump and the enemy is on the ground
            if (Time.time >= jumpTimer && IsGrounded() && Random.value <= chaserJumpChance)
            {
                rb.velocity = new Vector2(rb.velocity.x, chaserJumpForce);
                jumpTimer = Time.time + chaserJumpInterval;
            }
        }

        bool IsGrounded()
        {
            // Check if the enemy is grounded using a small raycast
            float rayLength = 0.1f;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);
            return hit.collider != null;
        }



    }
    public void EnemyRetreat()
    {
        if (!retreating)
        {
            enemyHealth.isImmune = true;
            retreating = true;
            Vector2 direction = (target.position - transform.position).normalized;
            //Debug.Log("Flinging");
            // Jump backwards
            if (direction.x > 0)
            {
                rb.AddForce(Vector2.left * chaserJumpForce * .8f, ForceMode2D.Impulse);
            }
            else if (direction.x < 0)
            {
                rb.AddForce(Vector2.right * chaserJumpForce * .8f, ForceMode2D.Impulse);
            }
            rb.velocity = new Vector2(rb.velocity.x, chaserJumpForce * .75f);

            Invoke("StopRetreat", chaserRetreatTime);
        }
        
    }
    
    private void StopRetreat()
    {
        //Debug.Log("Stopped Retreating");
        retreating = false;
        enemyHealth.isImmune = false;
    }
    public void TakeDamage(float damage)
    {
        retreating = true;
        // Calculate the fling force based on the damage percentage
        float damagePercentage = damage / enemyHealth.enemyMaxHealth; // Assuming maxHealth is a known float value
        float flingForce = damagePercentage * maxFlingForce + 8f;
        // Calculate the direction from the player to the enemy
        Vector2 attackDirection = (transform.position - target.position).normalized;

        // Apply the fling force in the direction of the attack
        if(attackDirection.x > 0)
        {
            rb.AddForce(Vector2.right * flingForce, ForceMode2D.Impulse);
        }
        else if (attackDirection.x < 0)
        {
            rb.AddForce(Vector2.left * flingForce, ForceMode2D.Impulse);
        }
        rb.velocity = new Vector2(rb.velocity.x, flingForce * .5f);
        Invoke("StopRetreat", chaserRetreatTime/2 + damagePercentage * 1.3f);
    }

    IEnumerator DecelerateAndTurnAround()
    {
        retreating = true;
        // Wait for a brief moment before decelerating
        yield return new WaitForSeconds(0.3f);

        // Decelerate the enemy's horizontal velocity
        float decelerationAmount = chaserDeceleration * Time.deltaTime;
        float newXVelocity = Mathf.MoveTowards(rb.velocity.x, 0f, decelerationAmount);
        rb.velocity = new Vector2(newXVelocity, rb.velocity.y);

        // Wait for another brief moment before turning around
        yield return new WaitForSeconds(0.2f);

        // Turn the enemy around
        //Vector3 newScale = transform.localScale;
        //newScale.x = -newScale.x;
        //transform.localScale = newScale;
        StopRetreat();
    }

    void Shooter()
    {
         float distance = Vector2.Distance(transform.position, target.position);

        // If the player is within range, shoot at the player
        if (distance <= shooterMaxDistance)
        {
            // Update attack cooldown timer
            shooterAttackCooldownTimer -= Time.deltaTime;

            // If attack cooldown has elapsed, shoot at the player
            if (shooterAttackCooldownTimer <= 0)
            {
                ShootAtTarget(target.position);

                // Reset attack cooldown timer
                shooterAttackCooldownTimer = Random.Range(shooterAttackCooldownMin, shooterAttackCooldownMax);
            }
        }

        // If the player is out of range, move towards the player
        else
        {
            Vector2 direction = new Vector2(Mathf.Sign(target.position.x - transform.position.x), 0);

            // Calculate the speed of the enemy based on the distance to the player
            float targetSpeed = Mathf.Clamp(shooterAcceleration * distance / shooterMaxDistance, shooterSpeed, shooterAcceleration);

            // Move towards the player
            Vector2 newVelocity = direction * targetSpeed;
            rb.velocity = newVelocity;
        }
    }

    void ShootAtTarget(Vector2 targetPosition)
    {
        // Create projectile and set its position and rotation
        GameObject newProjectile = Instantiate(shooterProjectilePrefab, transform.position, Quaternion.identity);
        newProjectile.transform.right = targetPosition - (Vector2)transform.position;

        // Add force to the projectile in the direction of the player
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        newProjectile.GetComponent<Rigidbody2D>().AddForce(direction * 10f, ForceMode2D.Impulse);
    }
}
