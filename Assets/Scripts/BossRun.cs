using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : StateMachineBehaviour
{
    private Transform playerTransform;
    public float followDistance = 5f;
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
        // Calculate the distance to the player
        float distanceToPlayer = Vector3.Distance(playerTransform.position, animator.transform.position);

        // If the player is within the follow distance, start following
        if (distanceToPlayer <= followDistance)
        {
            isFollowing = false;
        }
		else
		{
			isFollowing = true;
		}

        // If the enemy is following the player, move towards them
        if (isFollowing)
        {
            // Calculate the direction to the player
            Vector3 directionToPlayer = playerTransform.position - animator.transform.position;

            // Move towards the player
            animator.transform.Translate(directionToPlayer.normalized * Time.deltaTime * followSpeed, Space.World);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Stop following the player when this state is exited
        isFollowing = false;
    }
}
