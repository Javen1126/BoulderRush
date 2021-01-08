using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 originalPos;
    public PlayerMovement movement;
    void Start()
    {
        originalPos = gameObject.transform.position;
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "LoseCon")
        {
            Debug.Log("We hit an obstacle!");
            //movement.enabled = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            transform.position = originalPos;
        }
    }
}
