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
    public bool Invincible = false;
    public bool SMultiply = false;
    public float duration;
    public float sizeMultiplier;
    void Start()
    {
        originalPos = gameObject.transform.position;
    }
    void OnCollisionEnter(Collision collisionInfo)
    {
        if(collisionInfo.collider.tag == "LoseCon")
        {
            if(Invincible == false)
            {
                Debug.Log("We hit an obstacle!");
                FindObjectOfType<AudioManager>().PlaySound("GameOver");
                FindObjectOfType<GameManager>().GameOver();
                movement.enabled = false;
            }
            else
            {
                Debug.Log("We destroys something!");
                FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
                FindObjectOfType<ScoreUI>().destroyPoint();
                Destroy(collisionInfo.gameObject);
            }

            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //transform.position = originalPos;
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Breakable")
        {
            if (SMultiply == false)
            {
                Debug.Log("We destroys something!");
                FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
                FindObjectOfType<ScoreUI>().destroyPoint();
                Destroy(other.gameObject);
            }
            else
            {
                Debug.Log("We destroys something with Score Multiplier!");
                FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
                FindObjectOfType<ScoreUI>().destroyPoint(2);
                Destroy(other.gameObject);
            }
        }

        if (other.tag == "Invincible")
        {
            Debug.Log("We got Invincible Power Up!");
            StartCoroutine(InvinciblePow(rb));
            Destroy(other.gameObject);
        }

        if (other.tag == "ScoreMultiplier")
        {
            Debug.Log("We got ScoreMultiplier Power Up!");
            StartCoroutine(SMultiplierPow(rb));
            Destroy(other.gameObject);
        }
    }

    IEnumerator InvinciblePow(Rigidbody player)
    {
        player.transform.localScale *= sizeMultiplier;
        Invincible = true;
        yield return new WaitForSeconds(duration);
        Invincible = false;
        player.transform.localScale /= sizeMultiplier;
    }

    IEnumerator SMultiplierPow(Rigidbody player)
    {
        SMultiply = true;
        yield return new WaitForSeconds(duration);
        SMultiply = false;
    }

}
