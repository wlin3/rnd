using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingIdle : StateMachineBehaviour
{

    public float speed = 10f;
    public float dashDistance = 10f;
    public float dashDuration = 0.5f;
    public float dashCooldown = 2f;
    public float dashHeight = 5f;

    private Transform playerTransform;
    private bool canDash = true;
    private MonoBehaviour script;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        script = animator.GetComponent<MonoBehaviour>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (canDash)
        canDash = true;        
        {
            // Calculate the direction towards the player
            Vector3 direction = playerTransform.position - animator.transform.position;
            direction.y = 0f;

            // Dash towards the player
            script.StartCoroutine(Dash(animator, direction.normalized));

            // Reset dash cooldown
            canDash = false;
        }
        
    }

    private IEnumerator Dash(Animator animator, Vector3 direction)
    {
        // Store the initial position
        Vector3 initialPosition = animator.transform.position;

        // Calculate the target position
        Vector3 targetPosition = animator.transform.position + direction * dashDistance;
        targetPosition.y = playerTransform.position.y + 10f;

        // Calculate the duration of the dash
        float dashTime = 0f;
        while (dashTime < dashDuration)
        {
            // Update the position of the enemy
            animator.transform.position = Vector3.Lerp(initialPosition, targetPosition, dashTime / dashDuration);

            // Increment the dash time
            dashTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Snap to the target position
        animator.transform.position = targetPosition;
    }


}
