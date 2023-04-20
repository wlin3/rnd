using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingPatrol : StateMachineBehaviour
{
    private Transform playerTransform;
    public float minDistance = 3f;
    public float maxDistance = 10f;
    public float followSpeed = 2f;

    private bool isFollowing;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Start following the player when this state is entered
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        isFollowing = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Calculate the direction and distance to the player
        Vector3 directionToPlayer = playerTransform.position - animator.transform.position;
        float distanceToPlayer = directionToPlayer.magnitude;

        // If the enemy is too close or too far from the player, stop following
        if (distanceToPlayer < minDistance || distanceToPlayer > maxDistance)
        {
            isFollowing = false;
        }

        // If the enemy is following the player, move towards them
        if (isFollowing)
        {
            animator.transform.Translate(directionToPlayer.normalized * Time.deltaTime * followSpeed, Space.World);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop following the player when this state is exited
        isFollowing = false;
    }
}
