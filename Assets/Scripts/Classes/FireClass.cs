using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireClass : MonoBehaviour
{
    public string fireTag; // Specify the tag of the burning object
    public float hp = 1000;
    public PlayerMovement playerMovement;

    // Reference to the original (unburned) prefab
    public GameObject originalPrefab;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            playerMovement.knockbackDuration = playerMovement.knockbackTotalTime;
            if(collision.transform.position.x <= transform.position.x)
            {
                playerMovement.knockFromRight = true;
            }
            if(collision.transform.position.x > transform.position.x)
            {
                playerMovement.knockFromRight = false;
            }
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
