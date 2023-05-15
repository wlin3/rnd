using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    private float moveInput;
    public float jumpForce;
    public float initialJumpForce;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private float jumpTimeCounter;
    public float jumpTime;
    private bool isJumping;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow)) && isGrounded && isJumping == false)
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, initialJumpForce);
        }
        if((Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.Space) || Input.GetKeyUp(KeyCode.UpArrow)))
            {
                isJumping = false;
            }

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.UpArrow)) && isJumping == true)
        {
            if (jumpTimeCounter > 0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            
            else
            {
                isJumping = false;
            }
        }
    }

    bool IsGrounded()
    {
        // Check if the enemy is grounded using a small raycast
        float rayLength = 0.2f;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, rayLength);
        return hit.collider != null;
    }
}
