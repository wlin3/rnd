using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public int damage = 10;
    private Rigidbody2D rb;
    public float force;
    private bool facingRight = true;
    public float rotationDegree = 0f; // Set rotation for each projectile

    public bool canDecelerate;
    public float distance;
    public float decelerationTime;
    public float lingerTime;

    private List<Collider2D> hitEnemies = new List<Collider2D>();

    private float initialSpeed;
    private float decelerationRate;
    private float decelerationDistance;
    private bool isDecelerating;
    private float lingerTimer;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - playerTransform.position.x, mousePosition.y - playerTransform.position.y);
        rb = GetComponent<Rigidbody2D>();

        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        if (direction.x < 0 && facingRight)
        {
            facingRight = false;
        }

        else if (direction.x > 0 && !facingRight)
        {
            facingRight = true;
        }
        transform.rotation = Quaternion.Euler(0, 0, rot + rotationDegree);

        if (canDecelerate)
        {
            initialSpeed = force;
            decelerationRate = initialSpeed / decelerationTime;
            decelerationDistance = distance / 2f;
            isDecelerating = false;
            lingerTimer = 0f;
        }

        Destroy(gameObject, distance / force);
    }

    void Update()
    {
        // Calculate the angle between the current velocity and the x-axis
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        
        // Update the rotation of the sprite
        transform.rotation = Quaternion.AngleAxis(angle + rotationDegree, Vector3.forward);
        if (canDecelerate && !isDecelerating)
        {
            float distanceTravelled = Vector3.Distance(transform.position, transform.position + (Vector3)rb.velocity * Time.deltaTime);

            if (distanceTravelled >= decelerationDistance)
            {
                isDecelerating = true;
                rb.velocity = rb.velocity.normalized * initialSpeed;
            }
        }
        else if (canDecelerate && isDecelerating)
        {
            rb.velocity = Vector2.MoveTowards(rb.velocity, Vector2.zero, decelerationRate * Time.deltaTime);
            if (rb.velocity.magnitude <= 0f)
            {
                lingerTimer += Time.deltaTime;
                if (lingerTimer >= lingerTime)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !hitEnemies.Contains(collision))
        {
            hitEnemies.Add(collision);

            EnemyHealth enemy = collision.GetComponent<EnemyHealth>();
            if (enemy != null && !enemy.isImmune)
            {
                enemy.TakeDamageEnemy(damage);
            }
        }
        else if (collision.CompareTag("Boss") && !hitEnemies.Contains(collision))
        {
            hitEnemies.Add(collision);

            BossHealth enemy = collision.GetComponent<BossHealth>();
            if (enemy != null && !enemy.BossisImmune)
            {
                enemy.BossTakeDamage(damage);
            }

        }
    }
}