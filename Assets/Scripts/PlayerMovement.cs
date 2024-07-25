using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    // private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private GameObject extinguisherPrefab;
    [SerializeField] private GameObject replacementPrefab;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform shootPoint;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private float knockbackForce = 10f;
    private bool hasExtinguisher = false;

    private GameObject carriedExtinguisher = null;

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
    }

    private void HandleMovement()
    {
        dirX = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        if (Input.GetKeyDown(KeyCode.S))
        {
            // Code to go down a platform
            Collider2D platform = Physics2D.OverlapCircle(transform.position, 0.1f, platformLayer);
            if (platform != null)
            {
                StartCoroutine(DisablePlatformCollider(platform));
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            // Code to go into wall door
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, 1f);
            if (hit.collider != null && hit.collider.CompareTag("Door"))
            {
                // Implement door interaction logic
                Debug.Log("Entered door");
            }
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void HandleExtinguisher()
    {
        if (Input.GetMouseButtonDown(0) && hasExtinguisher)
        {
            // Shoot extinguisher
            // Instantiate(extinguisherPrefab, shootPoint.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (IsOverExtinguisher(out GameObject groundExtinguisher))
            {
                // Pickup or swap extinguisher
                if (carriedExtinguisher != null)
                {
                    SwapExtinguisher(groundExtinguisher);
                }
                else
                {
                    PickupExtinguisher(groundExtinguisher);
                }
            }
            else if (hasExtinguisher)
            {
                // Drop extinguisher
                DropExtinguisher();
            }
            
        }
    }

    // private void OnCollisionEnter2D(Collision2D collision)
    // {
    //     if (collision.gameObject.tag.StartsWith("Fire")) // General fire type check using tags
    //     {
    //         FireClass fireClass = collision.gameObject.GetComponent<FireClass>();

    //         if (fireClass != null && carriedExtinguisher != null)
    //         {
    //             string extinguisherName = carriedExtinguisher.name; // Assuming the extinguisher name is the same as its GameObject name
    //             if (FireManager.CanExtinguish(fireClass.fireType, extinguisherName))
    //             {
    //                 fireClass.hp -= 10f; // Adjust the damage as needed
    //                 if (fireClass.hp <= 0)
    //                 {
    //                     fireClass.Extinguish(); // Replace the burned prefab with the original
    //                 }
    //             }
    //             else
    //             {
    //                 KnockbackPlayer(collision.transform);
    //             }
    //         }
    //         else
    //         {
    //             Debug.LogError("FireClass component not found or carriedExtinguisher is null.");
    //         }
    //     }
    // }


    private void KnockbackPlayer(Transform fireTransform)
    {
        Vector2 knockbackDirection = (transform.position - fireTransform.position).normalized;
        GetComponent<Rigidbody2D>().AddForce(knockbackDirection * knockbackForce, ForceMode2D.Impulse);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsOverExtinguisher(out GameObject extinguisher)
    {
        // Get the player's collider
        Collider2D playerCollider = GetComponent<Collider2D>();

        // Temporarily disable the player's collider
        playerCollider.enabled = false;

        // Define the raycast origins and direction
        Vector2 raycastOrigin = transform.position;
        Vector2 raycastDirectionRight = Vector2.right;
        Vector2 raycastDirectionLeft = Vector2.left;

        // Perform the raycasts
        float raycastDistance = 1.0f; // Adjust as needed
        RaycastHit2D hitFromRight = Physics2D.Raycast(raycastOrigin, raycastDirectionRight, raycastDistance);
        RaycastHit2D hitFromLeft = Physics2D.Raycast(raycastOrigin, raycastDirectionLeft, raycastDistance);

        // Re-enable the player's collider
        playerCollider.enabled = true;

        // Debugging raycast information
        Debug.DrawRay(raycastOrigin, raycastDirectionRight * raycastDistance, Color.red, 2f);
        Debug.DrawRay(raycastOrigin, raycastDirectionLeft * raycastDistance, Color.red, 2f);

        // Check if the raycast from the right hit an extinguisher
        if (hitFromRight.collider != null)
        {
            Debug.Log("Raycast hit from right: " + hitFromRight.collider.name);
            if (hitFromRight.collider.CompareTag("Extinguisher") && hitFromRight.collider.gameObject != carriedExtinguisher)
            {
                extinguisher = hitFromRight.collider.gameObject;
                Debug.Log("Extinguisher found on the right: " + extinguisher.name);
                return true;
            }
        }

        // Check if the raycast from the left hit an extinguisher
        if (hitFromLeft.collider != null)
        {
            Debug.Log("Raycast hit from left: " + hitFromLeft.collider.name);
            if (hitFromLeft.collider.CompareTag("Extinguisher") && hitFromLeft.collider.gameObject != carriedExtinguisher)
            {
                extinguisher = hitFromLeft.collider.gameObject;
                Debug.Log("Extinguisher found on the left: " + extinguisher.name);
                return true;
            }
        }

        extinguisher = null;
        return false;
    }

    private void PickupExtinguisher(GameObject extinguisher)
    {
        carriedExtinguisher = extinguisher;
        carriedExtinguisher.SetActive(true); // Make sure it's active
        carriedExtinguisher.transform.position = holdPoint.position; // Set position to the holdPoint
        carriedExtinguisher.transform.parent = holdPoint; // Parent it to the holdPoint
        hasExtinguisher = true;
        Debug.Log("Picked up extinguisher");
    }

    private void DropExtinguisher()
    {
        carriedExtinguisher.SetActive(true);
        carriedExtinguisher.transform.parent = null; // Unparent the extinguisher
        carriedExtinguisher.transform.position = transform.position; // Drop at player's position
        carriedExtinguisher = null;
        hasExtinguisher = false;
        Debug.Log("Dropped extinguisher");
    }

    private void SwapExtinguisher(GameObject groundExtinguisher)
    {
        Debug.Log("Swapping extinguishers");

        // Unparent the current extinguisher and place it at the ground extinguisher's position
        carriedExtinguisher.transform.parent = null; // Unparent the current extinguisher
        carriedExtinguisher.transform.position = groundExtinguisher.transform.position; // Position it at ground extinguisher's position
        carriedExtinguisher.SetActive(true); // Ensure it is active in the scene

        // Deactivate the ground extinguisher and set it as the new carried extinguisher
        groundExtinguisher.SetActive(false); // Deactivate the ground extinguisher
        carriedExtinguisher = groundExtinguisher; // Set ground extinguisher as the carried extinguisher

        // Parent the new carried extinguisher to the hold point and position it correctly
        carriedExtinguisher.transform.position = holdPoint.position; // Position new extinguisher at the holdPoint
        carriedExtinguisher.transform.parent = holdPoint; // Parent the new extinguisher to holdPoint
        carriedExtinguisher.SetActive(true); // Ensure it is active in the scene

        Debug.Log("Swapped extinguisher");
    }


    private IEnumerator DisablePlatformCollider(Collider2D platform)
    {
        Collider2D platformCollider = platform.GetComponent<Collider2D>();
        platformCollider.enabled = false;
        yield return new WaitForSeconds(0.5f);
        platformCollider.enabled = true;
    }
}
