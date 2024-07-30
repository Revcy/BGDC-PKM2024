using System;
using UnityEngine;

public class SprayHandler : MonoBehaviour
{
    private string extinguisherName;

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
                    // Debug.Log("Go to else statement");
                    PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
                    playerMovement.HandleKnockBack();
                }
            }
        }
    }
}
