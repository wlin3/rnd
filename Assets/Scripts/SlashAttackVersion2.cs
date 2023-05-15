using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttackVersion2 : MonoBehaviour
{
    public float damage = 10f;
    private bool facingRight = true;
    public float lifetime = 2f; // set the lifetime of the projectile
    private Transform playerTransform;
    private Transform projectileLocation;
    private List<Collider2D> hitEnemies = new List<Collider2D>();

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        projectileLocation = GameObject.Find("Projectile Location").transform;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(mousePosition.x - playerTransform.position.x, mousePosition.y - playerTransform.position.y);

        // Vector3 rotation = transform.position - mousePosition;

        // float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        if (direction.x < 0 && facingRight)
        {
            facingRight = false;
            transform.localScale = new Vector3(1, -1, 1);
            transform.Rotate(0, 0, 180);
            direction = new Vector2(-direction.x, -direction.y);
        }
         else if (direction.x > 0 && !facingRight)
         {
            facingRight = true;
            transform.Rotate(0, 0, 180);
            transform.localScale = new Vector3(1, 1, 1);
         }
         //transform.rotation = Quaternion.Euler(0, 0, rot + 180);

        Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 direction = mousePosition - playerTransform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.z = 0;
            direction.Normalize();
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.position = projectileLocation.position;
            transform.SetParent(projectileLocation, true);
        }
       
    }
    private void OnDestroy()
    {
        if (gameObject.activeInHierarchy)
        {
            transform.parent = null;
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
                // Check if the collision object has the "Boss" tag before executing the code
                if (!collision.CompareTag("Boss"))
                {
                    return;
                }

                hitEnemies.Add(collision);

                BossHealth enemy = collision.GetComponent<BossHealth>();
                if (enemy != null && !enemy.BossisImmune)
                {
                    enemy.BossTakeDamage(damage);
                }
            }
    }

}