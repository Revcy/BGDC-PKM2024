using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private SpriteRenderer sprite;
    private Animator anim;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private LayerMask platformLayer;
    [SerializeField] private GameObject extinguisherPrefab;
    [SerializeField] private GameObject replacementPrefab;
    [SerializeField] private Transform holdPoint;
    [SerializeField] private Transform sprayPoint;
    [SerializeField] private GameObject sprayPrefab;
    private Coroutine sprayCoroutine;
    private GameObject currentSpray;
    private SprayHandler currentSprayHandler;

    private float dirX = 0f;
    [SerializeField] private float moveSpeed = 7f;
    public float knockbackForce;
    public float knockbackDuration;
    public float knockbackTotalTime;
    private bool isKnockBack = false;
    private bool isShooting = false;
    private bool grounded = false;
    [SerializeField] private float jumpForce = 6f;
    private bool hasExtinguisher = false;
    public bool knockFromRight;
    private bool facingRight = true;
    private GameObject carriedExtinguisher = null;
    private enum MovementState { idle, walking, jumping, knockback, spraying }
    public GameObject FoamUI, CO2UI, DryChemUI, WaterUI, WetChemUI, DryPowUI;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (knockbackDuration > 0)
        {
            Debug.Log(knockbackDuration);
            isKnockBack = true;
            HandleKnockBack();
            UpdateAnimationState();
        }
        else
        {
            HandleMovement();
            HandleJump();
            HandleExtinguisher();
            UpdateAnimationState();
        }
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if(dirX > 0f || dirX < 0f)
        {
            state = MovementState.walking;
        }
        else
        {
           state = MovementState.idle;
        }

        if(isKnockBack == true)
        {
            state = MovementState.knockback;
        }
        else if(rb.velocity.y >= .1f || rb.velocity.y <= -.1f){
            state = MovementState.jumping;
        }

        if(isShooting == true && isKnockBack == false)
        {
            state = MovementState.spraying;
        }

        anim.SetInteger("state", (int)state);
    }

    private void HandleMovement()
    {
        // Debug.Log(isKnockBack);
        if(isKnockBack == false)
        {
            dirX = Input.GetAxis("Horizontal");
            rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.S))
            {
                // Code to go down a platform
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 2f, platformLayer);
                if (hit.collider != null)
                {
                    StartCoroutine(DisablePlatformCollider(hit.collider));
                }
            }

            if (dirX > 0 && !facingRight)
            {
                Flip();
            }
            else if (dirX < 0 && facingRight)
            {
                Flip();
            }
        }
        else{
            grounded = IsGrounded();
            if(grounded == true)
            {
                isKnockBack = false;
            }
        }
    }

    public void HandleKnockBack()
    {
        isKnockBack = true;
        if(knockFromRight == true)
        {
            Debug.Log(rb.velocity);
            rb.velocity = new Vector2(-knockbackForce, knockbackForce);
        }
        else if(knockFromRight == false)
        {
            Debug.Log(rb.velocity);
            rb.velocity = new Vector2(knockbackForce, knockbackForce);
        }
        knockbackDuration -= Time.deltaTime;
    }

    private void HandleJump()
    {
        if(isShooting == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && (IsGrounded() || IsPlatform()))
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            }
        }
    }

    private void HandleExtinguisher()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isShooting = true;
            StartShooting();
        }
        if (Input.GetMouseButtonUp(0))
        {
            isShooting = false;
            StopShooting();
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

    private void StartShooting()
    {
        grounded = IsGrounded();
<<<<<<< Updated upstream
        if (sprayCoroutine == null && carriedExtinguisher != null && (grounded == true || IsPlatform() == true))
=======
        if (sprayCoroutine == null && carriedExtinguisher != null && (grounded == true|| IsPlatform() == true))
>>>>>>> Stashed changes
        {
            sprayCoroutine = StartCoroutine(Shoot());
        }
    }

    private void StopShooting()
    {
        if (sprayCoroutine != null)
        {
            StopCoroutine(sprayCoroutine);
            sprayCoroutine = null;

            // Destroy the current spray object
            if (currentSpray != null)
            {
                Destroy(currentSpray);
                currentSpray = null;
            }
        }
    }

    private IEnumerator Shoot()
    {
        while (true)
        {
            if (carriedExtinguisher != null)
            {
                ExtinguisherDetails extinguisherScript = carriedExtinguisher.GetComponent<ExtinguisherDetails>();
                if (extinguisherScript != null)
                {
                    // If there is no current spray, instantiate a new one
                    if (currentSpray == null)
                    {
                        currentSpray = Instantiate(extinguisherScript.sprayPrefab, sprayPoint.position, sprayPoint.rotation, sprayPoint);
                        currentSprayHandler = currentSpray.GetComponent<SprayHandler>();
                        if (currentSprayHandler != null)
                        {
                            currentSprayHandler.SetExtinguisherName(carriedExtinguisher.name);
                        }
                    }

                    // Ensure the spray prefab is active
                    currentSpray.SetActive(true);

                    // Update the position of the spray to match the spray point
                    currentSpray.transform.position = sprayPoint.position;
                    currentSpray.transform.rotation = sprayPoint.rotation;

                    // Debugging logs
                    // Debug.Log("Spray instantiated: " + currentSpray.name);
                    // Debug.Log("Parent of spray: " + currentSpray.transform.parent.name);
                    // Debug.Log("Spray position: " + currentSpray.transform.position);
                }
                else
                {
                    Debug.LogError("ExtinguisherDetails script not found on carriedExtinguisher.");
                }
            }
            else
            {
                Debug.LogError("No carriedExtinguisher found.");
            }

            yield return null; // Wait for the next frame
        }
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
    }

    private bool IsPlatform()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, platformLayer);
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
        if(carriedExtinguisher.name == "Foam")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(true);
        }
        else if(carriedExtinguisher.name == "WBucket")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(true);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "WetChem")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(true);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "DryChem")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(true);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "DryPow")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(true);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "CO2")
        {
            CO2UI.SetActive(true);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
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
        CO2UI.SetActive(false);
        DryPowUI.SetActive(false);
        DryChemUI.SetActive(false);
        WetChemUI.SetActive(false);
        WaterUI.SetActive(false);
        FoamUI.SetActive(false);
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

        if(carriedExtinguisher.name == "Foam")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(true);
        }
        else if(carriedExtinguisher.name == "WBucket")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(true);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "WetChem")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(true);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "DryChem")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(true);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "DryPow")
        {
            CO2UI.SetActive(false);
            DryPowUI.SetActive(true);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        else if(carriedExtinguisher.name == "CO2")
        {
            CO2UI.SetActive(true);
            DryPowUI.SetActive(false);
            DryChemUI.SetActive(false);
            WetChemUI.SetActive(false);
            WaterUI.SetActive(false);
            FoamUI.SetActive(false);
        }
        Debug.Log("Swapped extinguisher");
    }

    private IEnumerator DisablePlatformCollider(Collider2D platform)
    {
        Collider2D platformCollider = platform.GetComponent<Collider2D>();
        platformCollider.enabled = false;
        yield return new WaitForSeconds(1f);
        platformCollider.enabled = true;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing
        facingRight = !facingRight;

        transform.Rotate(0f, 180f, 0f);
    }
}