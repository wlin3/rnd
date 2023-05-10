using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossShooting : MonoBehaviour
{

    public float fireballDuration = 3f;
    public float fireballSpeed = 5f;
    public float fireballCooldown = 2f;
    public GameObject Fireball;

    private GameObject player;
    private float timeSinceLastFireball = 0f;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    void Update()
    {
        // Move towards the player until within range
        // Summon fireball above the player
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
