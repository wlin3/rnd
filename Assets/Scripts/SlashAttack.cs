using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;
    private bool facingRight = true;
    public float lifetime = 2f; // set the lifetime of the projectile

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
        transform.rotation = Quaternion.Euler(0, 0, rot+180);

        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.parent = GameObject.FindWithTag("Player").transform;
    }
}
