using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrol : StateMachineBehaviour
{
    public float speed = 5f;

    private Transform playerTransform;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the current position of the enemy
        Vector3 currentPosition = animator.transform.position;

        // Get the position of the player
        Vector3 playerPosition = playerTransform.position;
        playerPosition.y = currentPosition.y; // Only follow x value of player

        // Calculate the direction towards the player
        Vector3 direction = (playerPosition - currentPosition).normalized;

        // Calculate the target position
        Vector3 targetPosition = currentPosition + direction * speed * Time.deltaTime;
        targetPosition.y = currentPosition.y; // Only update x value

        // Move the enemy towards the target position
        animator.transform.position = targetPosition;
    }
}