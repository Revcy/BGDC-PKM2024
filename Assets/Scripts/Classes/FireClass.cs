using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireClass
{
    public string extinguisherName;
    public Transform transform;
    public float speed = 100;
    public float hp = 100;

    void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.name == "Player"){
            Vector3 direction = (transform.position - collision.transform.forward).normalized;
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
        }
    }
}
