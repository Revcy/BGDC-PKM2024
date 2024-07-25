using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireClass : MonoBehaviour
{
    public string fireType; // Specify the detailed type of fire (e.g., "FireA1", "FireA2")
    public float hp = 100;

    // Reference to the original (unburned) prefab
    public GameObject originalPrefab;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = (transform.position - collision.transform.position).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = direction * 100; // Adjust the speed as needed
        }
    }

    public void Extinguish()
    {
        // Replace the burned prefab with the original prefab
        if (originalPrefab != null)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Destroy(gameObject); // Destroy the burned prefab
            Instantiate(originalPrefab, position, rotation); // Instantiate the original prefab
        }
    }
}
