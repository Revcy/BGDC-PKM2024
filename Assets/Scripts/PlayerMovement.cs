using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    // private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private GameObject extinguisherPrefab;
    [SerializeField] private Transform shootPoint;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 6f;
    private bool hasExtinguisher = false;

    private enum MovementState { idle, running, jumping, falling }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        // anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        HandleMovement();
        HandleJump();
        HandleExtinguisher();
        HandlePause();
    }

    private void HandleMovement()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Code to go down a platform, e.g., disable platform collider
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Code to go into wall door, e.g., interact with door
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void HandleExtinguisher()
    {
        if (Input.GetMouseButtonDown(0) && hasExtinguisher)
        {
            // Shoot extinguisher
            Instantiate(extinguisherPrefab, shootPoint.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Drop extinguisher
            hasExtinguisher = false;
        }

        if (Input.GetMouseButtonDown(1) && IsOverExtinguisher())
        {
            // Swap extinguisher
            hasExtinguisher = true;
        }
    }

    private void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 1f)
            {
                Time.timeScale = 0f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsOverExtinguisher()
    {
        // Check if the player is over an extinguisher on the ground
        return false; // Implement this based on your game logic
    }

    // private void UpdateAnimationState()
    // {
    //     MovementState state;

    //     if(dirX > 0f){
    //         state = MovementState.running;
    //         sprite.flipX = false;
    //     }
    //     else if(dirX < 0f){
    //         state = MovementState.running;
    //         sprite.flipX = true;
    //     }
    //     else{
    //         state = MovementState.idle;
    //     }

    //     if(rb.velocity.y > .1f){
    //         state = MovementState.jumping;
    //     }
    //     else if(rb.velocity.y < -.1f){
    //         state = MovementState.falling;
    //     }

    //     anim.SetInteger("state", (int)state);
    // }
}
