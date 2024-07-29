using System.Collections.Generic;
using UnityEngine;

public class PressPlate : MonoBehaviour
{
    public GameObject gate;
    private List<Collider2D> objectsInside = new List<Collider2D>();
    public Sprite Free;
    public Sprite Pressed;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!objectsInside.Contains(other))
        {
            objectsInside.Add(other);
        }

        // If this is the first object to enter, close the gate
        if (objectsInside.Count > 0 && gate != null)
        {
            gate.SetActive(false);

            Debug.Log("Gate Closed");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (objectsInside.Contains(other))
        {
            objectsInside.Remove(other);
        }

        // If there are no objects left inside, open the gate
        if (objectsInside.Count == 0 && gate != null)
        {
            gate.SetActive(true);
            Debug.Log("Gate Opened");
        }
    }
}
