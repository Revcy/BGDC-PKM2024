using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguisherClass : MonoBehaviour
{
    public string extinguisherName;
    public static List<GameObject> fireType = new List<GameObject>(); // Initialize the list
    public GameObject player;
    public float speed = 10f; // Define the speed variable

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Fire")
        {
            // Corrected Find method usage with a predicate
            GameObject matchingFireType = fireType.Find(fire => fire.name == collision.gameObject.name);

            if (matchingFireType != null)
            {
                // Access the FireClass component on the collided fire GameObject
                FireClass fireClass = collision.gameObject.GetComponent<FireClass>();

                if (fireClass != null && fireClass.extinguisherName == this.extinguisherName)
                {
                    // Reduce the fire's HP
                    fireClass.hp -= 10f; // Decrease HP by 10 (or any other value)
                    if (fireClass.hp <= 0)
                    {
                        Destroy(collision.gameObject); // Destroy the fire GameObject if HP is <= 0
                    }
                }
            }
            else
            {
                // Player gets knocked back
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                player.GetComponent<Rigidbody2D>().velocity = direction * speed;
            }
        }
    }
}
