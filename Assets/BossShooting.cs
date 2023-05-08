using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{
    public float speed = 2f;
    public float range = 5f;
    public float fireballDuration = 3f;
    public float fireballSpeed = 5f;
    public float fireballCooldown = 2f;
    public GameObject Fireball;

    private GameObject player;
    private bool isInRange = false;
    private float timeSinceLastFireball = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Move towards the player until within range
        if (!isInRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < range)
            {
                isInRange = true;
            }
        }
        // Summon fireball above the player
        else
        {
            if (timeSinceLastFireball >= fireballCooldown)
            {
                GameObject fireballPrefab = Instantiate(Fireball, player.transform.position + Vector3.up * 20f, Quaternion.identity);
                Rigidbody2D rb = fireballPrefab.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.down * fireballSpeed;
                Destroy(fireballPrefab, fireballDuration);
                timeSinceLastFireball = 0f;
            }
            else
            {
                timeSinceLastFireball += Time.deltaTime;
            }
        }
    }
}
