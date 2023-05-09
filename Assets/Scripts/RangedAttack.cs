using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : MonoBehaviour
{
    public int damage = 10;
    private Rigidbody2D rb;
    public float force;
    private bool facingRight = true;
    public float lifetime = 2f; // set the lifetime of the projectile
    private List<Collider2D> hitEnemies = new List<Collider2D>();
    public BossHealth BossHealth;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        rb = GetComponent<Rigidbody2D>();
        
        
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        if (direction.x < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * -1, transform.localScale.y, transform.localScale.z);
            transform.Rotate(0, 0, 180);
            direction = new Vector2(-direction.x, -direction.y);
        }
        else if (direction.x > 0 && !facingRight)
        {
            facingRight = true;
            transform.Rotate(0, 0, 180);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        transform.rotation = Quaternion.Euler(0, 0, rot+90);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (rb.velocity != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle+270, Vector3.forward);
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
            BossHealth.TakeDamage(1);
        }
    }
}