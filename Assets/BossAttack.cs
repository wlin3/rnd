using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : StateMachineBehaviour
{
    public float fireballDuration = 3f;
    public float fireballSpeed = 5f;
    public float fireballCooldown = 2f;
    public GameObject Fireball;

    private GameObject player;
    private float timeSinceLastFireball = 0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
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

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Clear any fireballs still active when exiting the state
        GameObject[] fireballs = GameObject.FindGameObjectsWithTag("Fireball");
        foreach (GameObject fireball in fireballs)
        {
            Destroy(fireball);
        }
    }
}
