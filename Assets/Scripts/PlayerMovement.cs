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
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private GameObject extinguisherPrefab;
    [SerializeField] private Transform shootPoint;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 6f;
    private bool hasExtinguisher = false;
    private bool isPaused = false;

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
        if (isPaused) return;

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
            Instantiate(extinguisherPrefab, shootPoint.position, Quaternion.identity);
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (hasExtinguisher)
            {
                // Drop extinguisher
                DropExtinguisher();
            }
            else if (IsOverExtinguisher(out GameObject groundExtinguisher))
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
        }
    }


    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsOverExtinguisher(out GameObject extinguisher)
    {
        Vector2 raycastOrigin = transform.position + new Vector3(1f, 0f, 0f); // Adjust as needed
        RaycastHit2D hit = Physics2D.Raycast(raycastOrigin, Vector2.left, 5f);
        if (hit.collider != null)
        {
            Debug.Log("Raycast hit: " + hit.collider.name);
            if (hit.collider.CompareTag("Extinguisher"))
            {
                extinguisher = hit.collider.gameObject;
                Debug.Log("Passed " + extinguisher.name);
                return true;
            }
        }
        extinguisher = null;
        return false;
    }


    private void PickupExtinguisher(GameObject extinguisher)
    {
        carriedExtinguisher = extinguisher;
        carriedExtinguisher.SetActive(false);
        hasExtinguisher = true;
        Debug.Log("Picked up extinguisher");
    }

    private void DropExtinguisher()
    {
        carriedExtinguisher.SetActive(true);
        carriedExtinguisher.transform.position = transform.position;
        carriedExtinguisher = null;
        hasExtinguisher = false;
        Debug.Log("Dropped extinguisher");
    }

    private void SwapExtinguisher(GameObject groundExtinguisher)
    {
        carriedExtinguisher.SetActive(true);
        carriedExtinguisher.transform.position = groundExtinguisher.transform.position;
        groundExtinguisher.SetActive(false);
        carriedExtinguisher = groundExtinguisher;
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
