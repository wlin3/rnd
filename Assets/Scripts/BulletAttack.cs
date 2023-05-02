using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public int damage = 10;
    private Rigidbody2D rb;
    public float force;
    public float lifetime = 2f;
    private bool facingRight = true;
    public float rotationDegree = 0f; // Set rotation for each projectile

    public float spreadAngle = 30f; // Maximum spread angle in degrees

    private List<Collider2D> hitEnemies = new List<Collider2D>();

    
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - playerTransform.position.x, mousePosition.y - playerTransform.position.y);
        rb = GetComponent<Rigidbody2D>();

        // Apply random spread to the direction
        float angle = Random.Range(-spreadAngle, spreadAngle);
        direction = Quaternion.Euler(0, 0, angle) * direction;

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

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Calculate the angle between the current velocity and the x-axis
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
        
        // Update the rotation of the sprite
        transform.rotation = Quaternion.AngleAxis(angle + rotationDegree, Vector3.forward);
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
    }
}
