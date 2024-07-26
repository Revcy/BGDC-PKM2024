using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireClass : MonoBehaviour
{
    public string fireTag; // Specify the tag of the burning object
    public float hp = 1000;

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
        if (originalPrefab != null)
        {
            Vector3 position = transform.position;
            Quaternion rotation = transform.rotation;
            Vector3 originalSize = transform.localScale;

            Destroy(gameObject);
            GameObject newPrefab = Instantiate(originalPrefab, position, rotation);
            newPrefab.transform.localScale = originalSize;

            Debug.Log("Instantiated originalPrefab at: " + position);
            Debug.Log("Scale of newPrefab: " + newPrefab.transform.localScale);
        }
    }
}
