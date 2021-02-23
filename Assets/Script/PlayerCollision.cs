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
    public float destroyTime;
    public int maxaddupPoint;
    public int maxscoreMultiplier = 5;
    public int scoreMultiplier;
    public int addupPoint = 0;
    public int requiredPoint;
    public GameObject floatingTextPrefab;
    void Start()
    {
        originalPos = gameObject.transform.position;
    }
    void OnCollisionEnter(Collision collisionInfo)
    {

        if (collisionInfo.collider.tag == "LoseCon")
        {
            if (Invincible == false)
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
                FindObjectOfType<ScoreUI>().destroyPoint(scoreMultiplier);
                if (floatingTextPrefab)
                {
                    showFloatingText(collisionInfo);
                }
                Destroy(collisionInfo.gameObject, destroyTime);
            }
        }

        if(collisionInfo.collider.tag == "Breakable")
        {
            // Calculate Angle Between the collision point and the player
            Vector3 dir = collisionInfo.contacts[0].point - rb.transform.position;
            // We then get the opposite (-Vector3) and normalize it
            dir = dir.normalized;
            // And finally we add force in the direction of dir and multiply it by force. 
            // This will push back the player
            collisionInfo.rigidbody.AddForce(dir * 20);
            Debug.Log("We destroys something!");
            FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
            FindObjectOfType<ScoreUI>().destroyPoint(scoreMultiplier);
            if (floatingTextPrefab)
            {
                showFloatingText(collisionInfo);
            }
            Destroy(collisionInfo.gameObject, destroyTime);
            if (Invincible == false)
            {
                addupPoint += (100 * scoreMultiplier);
            }
            Debug.Log(scoreMultiplier);
            Debug.Log(addupPoint);
            Debug.Log(requiredPoint);
            if (addupPoint >= requiredPoint)
            {
                if (scoreMultiplier >= maxscoreMultiplier)
                {
                    addupPoint = 0;
                    StartCoroutine(InvinciblePow(rb));
                }
                else
                {
                    scoreMultiplier += 1;
                    addupPoint = 0;
                    StartCoroutine(InvinciblePow(rb));
                    
                }
                if(requiredPoint < maxaddupPoint)
                {
                    requiredPoint *= 2;
                }
                else
                {
                    requiredPoint = maxaddupPoint;
                }
            }
        }

            //rb.velocity = Vector3.zero;
            //rb.angularVelocity = Vector3.zero;
            //transform.position = originalPos;
        if (collisionInfo.collider.tag == "Floor")
        {
            FindObjectOfType<PlayerMovement>().isGrounded = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Breakable")
        //{
        //    if (SMultiply == false)
        //    {
        //        Debug.Log("We destroys something!");
        //        FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
        //        FindObjectOfType<ScoreUI>().destroyPoint();
        //        Destroy(other.gameObject);
        //    }
        //    else
        //    {
        //        Debug.Log("We destroys something with Score Multiplier!");
        //        FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
        //        FindObjectOfType<ScoreUI>().destroyPoint(2);
        //        Destroy(other.gameObject);
        //    }
        //}

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
    public void showFloatingText(Collision other)
    {
        var go = Instantiate(floatingTextPrefab, other.transform.position, Quaternion.identity, other.transform);
        int parseScore = 100 * scoreMultiplier;
        go.GetComponent<TextMesh>().text = parseScore.ToString();
    }
}
