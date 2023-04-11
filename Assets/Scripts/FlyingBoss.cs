using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingBoss : MonoBehaviour
{
    public float speed = 10f;
    public float dashDistance = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float dashHeight = 5f;

    private Transform playerTransform;
    private bool canDash = true;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (canDash)
        {
            // Calculate the direction towards the player
            Vector3 direction = playerTransform.position - transform.position;
            direction.y = 0f;

            // Dash towards the player
            StartCoroutine(Dash(direction.normalized));

            // Reset dash cooldown
            canDash = false;
            Invoke(nameof(ResetDashCooldown), dashCooldown);
        }
    }

    private IEnumerator Dash(Vector3 direction)
    {
        // Store the initial position
        Vector3 initialPosition = transform.position;

        // Calculate the target position
        Vector3 targetPosition = transform.position + direction * dashDistance;
        targetPosition.y = playerTransform.position.y;

        // Calculate the duration of the dash
        float dashTime = 0f;
        while (dashTime < dashDuration)
        {
            // Update the position of the enemy
            transform.position = Vector3.Lerp(initialPosition, targetPosition, dashTime / dashDuration);

            // Increment the dash time
            dashTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Snap to the target position
        transform.position = targetPosition;
    }

    private void ResetDashCooldown()
    {
        canDash = true;
    }
}