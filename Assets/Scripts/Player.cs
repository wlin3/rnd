using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float walkspeed;
    [HideInInspector] public Rigidbody2D rb;
    private float moveInput;
    private Transform spriteTransform;
    private bool facingRight = true;
    
    // Start is called before the first frame update
    void Start()
    {
        spriteTransform = transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = new Vector2(mousePosition.x - spriteTransform.position.x, mousePosition.y - spriteTransform.position.y);
            moveInput = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(moveInput * walkspeed, rb.velocity.y);

            if (direction.x < 0 && facingRight) 
            {
                facingRight = false;
                spriteTransform.localScale = new Vector3(1, -1, 1);
                transform.Rotate(0, 0, 180);
                direction = new Vector2(-direction.x, -direction.y);
            } 
            else if(direction.x > 0 && !facingRight)
            {
                facingRight = true;
                transform.Rotate(0, 0, 180);
                spriteTransform.localScale = new Vector3(1, 1, 1);
            }
        }
        

        
    }
    
}
