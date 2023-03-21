using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingMadeByAi : MonoBehaviour
{
    public float jumpForceLow = 5f; // The force applied when tapping jump
    public float jumpForceHigh = 10f; // The force applied when holding jump
    public float jumpTimeThreshold = 0.2f; // The minimum time the jump button must be held to trigger a high jump
    public Transform groundCheck; // A transform that represents the position where the ground is checked
    public float groundCheckRadius = 0.1f; // The radius of the ground check sphere
    public LayerMask whatIsGround; // A layer mask that defines what counts as ground
    private Rigidbody2D rb; // The rigidbody component of the object
    private bool isGrounded; // Whether or not the object is touching the ground
    private float jumpTimeCounter; // The amount of time the jump button has been held

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForceLow);
            jumpTimeCounter = jumpTimeThreshold;
        }

        if (Input.GetKey(KeyCode.W) && jumpTimeCounter > 0 && !isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForceHigh);
            jumpTimeCounter -= Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            jumpTimeCounter = 0;
        }
    }
}
