using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    public int damage = 10;
    private Rigidbody2D rb;
    public float force;
    public float lifetime = 2f; // set the lifetime of the projectile
    private List<Collider2D> hitEnemies = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //rb.velocity = rb.velocity.normalized * force; // Use rb.velocity.normalized instead of transform.right
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Flip the projectile horizontally
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * Mathf.Sign(rb.velocity.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !hitEnemies.Contains(collision))
        {
            hitEnemies.Add(collision);

            PlayerHealth player = collision.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damage);
            }
        }
    }
}
