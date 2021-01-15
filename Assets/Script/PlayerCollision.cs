using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollision : MonoBehaviour
{
    public Rigidbody rb;
    Vector3 originalPos;
    public PlayerMovement movement;
    public Text scoreText;
    void Start()
    {
        originalPos = gameObject.transform.position;
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "LoseCon")
        {
            Debug.Log("We hit an obstacle!");
            FindObjectOfType<GameManager>().GameOver();
            movement.enabled = false;
            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //transform.position = originalPos;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Breakable")
        {
            Debug.Log("We destroys somethhing!");
            FindObjectOfType<ScoreUI>().destroyPoint();
            Destroy(other.gameObject);
        }
    }

}
