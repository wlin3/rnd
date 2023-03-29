using System.Collections;
using System.Collections.Generic;
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
    private float jumpTimer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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
        float distance = Vector2.Distance(transform.position, player.position);

        // If the player is within range, chase the player
        if (distance <= chaserMaxDistance)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            // Calculate the speed of the enemy based on the distance to the player
            float targetSpeed = Mathf.Lerp(chaserSpeed, chaserAcceleration, 1f - distance / chaserMaxDistance);

            // Accelerate the enemy towards the player
            rb.velocity = Vector2.MoveTowards(rb.velocity, direction * targetSpeed, chaserAcceleration * Time.deltaTime);

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
            if (Mathf.Sign(rb.velocity.x) != Mathf.Sign(direction.x))
            {
                // Decelerate the enemy and flip around
                rb.velocity = Vector2.MoveTowards(rb.velocity, -direction * targetSpeed, chaserDeceleration * Time.deltaTime);
            }
        }

        // Randomly jump if it's time to jump and the enemy is on the ground
        if (Time.time >= jumpTimer && IsGrounded() && Random.value <= chaserJumpChance)
        {
            rb.AddForce(Vector2.up * chaserJumpForce, ForceMode2D.Impulse);
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
