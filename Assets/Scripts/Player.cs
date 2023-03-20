using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] public float walkspeed;
    [HideInInspector] public Rigidbody2D rb;
    private float moveInput;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
        moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * walkspeed, rb.velocity.y);

        
    }
}
