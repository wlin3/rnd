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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyHealth = GetComponent<EnemyHealth>();

    }

    // Update is called once per frame
    void Update()
    {
        if (EnemyType == enemyType.Chaser)
        {
            Chaser();
        }
    }

    void Chaser()
    {

        if (retreating)
        {
            return;
        }
        float distance = Vector2.Distance(transform.position, player.position);
        
        // If the player is within range, chase the player
        if (distance <= chaserMaxDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Calculate the speed of the enemy based on the distance to the player
            float targetSpeed = Mathf.Clamp(chaserAcceleration * distance / chaserMaxDistance, chaserSpeed, chaserAcceleration);


            // Move towards the player
            Vector2 newVelocity = direction * targetSpeed;
            //Debug.Log(newVelocity.ToString());
            if (!retreating)
            {
                rb.velocity = new Vector2(newVelocity.x, rb.velocity.y);
            }
            //rb.velocity = new Vector2(newVelocity.x, rb.velocity.y);

            // Flip the enemy sprite if necessary
            if (direction.x > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
            }
            else if (direction.x < 0)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }

            // Check if the enemy has overshot the player
            if(Mathf.Sign(rb.velocity.x) != Mathf.Sign(direction.x) && Mathf.Abs(rb.velocity.x) > 50f)
            {
                // Calculate the overshoot direction
                Vector2 overshootDirection = (direction.x > 0) ? Vector2.right : Vector2.left;

                // Keep moving in the overshoot direction for a brief moment
                rb.velocity = Vector2.MoveTowards(rb.velocity, overshootDirection * targetSpeed, chaserDeceleration * Time.deltaTime);
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
            Vector2 direction = (player.position - transform.position).normalized;
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
        Vector2 attackDirection = (transform.position - player.position).normalized;

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
        Invoke("StopRetreat", chaserRetreatTime/2);
    }
}
