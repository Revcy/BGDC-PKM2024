using System;
using UnityEngine;

public class SprayHandler : MonoBehaviour
{
    private string extinguisherName;
    private float knockbackForce = 10f;

    public void SetExtinguisherName(string name)
    {
        extinguisherName = name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckAndApplyDamage(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        CheckAndApplyDamage(collision);
    }


    private void CheckAndApplyDamage(Collider2D collision)
    {
        if (collision.gameObject.tag.StartsWith("Fire"))
        {
            FireClass fireClass = collision.gameObject.GetComponent<FireClass>();

            if (fireClass != null)
            {
                if (FireManager.CanExtinguish(fireClass.fireTag, extinguisherName))
                {
                    fireClass.hp -= 10f; // Ensure this is the correct decrement
                    Debug.Log("Fire health: " + fireClass.hp); // Debugging log

                    if (fireClass.hp <= 0)
                    {
                        fireClass.Extinguish(); // Replace the burned prefab with the original
                    }
                }
                else
                {
                    Debug.Log("Go to else statement");
                    PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
                    if (playerMovement != null)
                    {
                        Debug.Log("About to knockback player");
                        KnockbackPlayer(playerMovement.transform, collision.transform);
                    }
                    else
                    {
                        Debug.LogError("PlayerMovement script not found.");
                    }
                }
            }
        }
    }

    private void KnockbackPlayer(Transform playerTransform, Transform fireTransform)
    {
        // Calculate the knockback direction
        Vector2 knockbackDirection = (playerTransform.position - fireTransform.position).normalized;

        // Add an upward component to the knockback direction
        knockbackDirection.y = Mathf.Abs(knockbackDirection.y) + 0.5f; // Adding 0.5f to ensure a significant upward force

        // Normalize the final direction to maintain consistent knockback force
        knockbackDirection = knockbackDirection.normalized;

        // Apply the knockback force
        Rigidbody2D rb = playerTransform.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = knockbackDirection * knockbackForce;
            Debug.Log("Knockback direction: " + knockbackDirection + ", force: " + knockbackForce);
        }
        else
        {
            Debug.LogError("Rigidbody2D not found on player.");
        }
    }
}
