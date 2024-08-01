using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public int distance = -10;
    public float offset = 1.0f;

    void Start()
    {
        GameManager.instance.InitializeSound();
    }
    void Update()
    {
        this.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + offset, distance);
    }
}
