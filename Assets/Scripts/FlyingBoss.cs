using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoss : MonoBehaviour
{
    private Transform playerTransform; // reference to the player's transform
    private SpriteRenderer spriteRenderer; // reference to the enemy's sprite renderer
    private float facingTimer = 0f; // timer to track when to face the player

    // Start is called before the first frame update
    void Start()
    {
        // find the player object and get its transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // get the enemy's sprite renderer component
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        // update the timer
        facingTimer += Time.deltaTime;

        // check if it's time to face the player
        if (facingTimer >= .25f)
        {
            // reset the timer
            facingTimer = 0f;

            // face the player
            FacePlayer();
        }
    }

    private void FacePlayer()
    {
        // calculate the direction to the player
        Vector2 direction = (playerTransform.position - transform.position).normalized;

        // calculate the rotation that the enemy needs to look at the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // check if the enemy should be flipped horizontally
        if (angle > 90f || angle < -90f)
        {
            spriteRenderer.flipX = true;
            angle += 180f;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}