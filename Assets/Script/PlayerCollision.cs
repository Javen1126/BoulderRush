﻿using System.Collections;
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
    public float duration;
    public float sizeMultiplier;
    public float destroyTime;
    public int maxaddupPoint;
    public int maxscoreMultiplier = 5;
    public int scoreMultiplier;
    public int addupPoint = 0;
    public int requiredPoint;
    public GameObject floatingTextPrefab;
    public GameObject Explosion;
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
                FindObjectOfType<AudioManager>().PlaySound("GameOver");
                StartCoroutine(Lose());
                movement.enabled = false;
                // Calculate Angle Between the collision point and the player
                Vector3 dir = collisionInfo.contacts[0].point - transform.position;
                // We then get the opposite (-Vector3) and normalize it
                dir = -dir.normalized;
                // And finally we add force in the direction of dir and multiply it by force. 
                // This will push back the player
                rb.AddForce(dir * 500);
                Explode(collisionInfo);
                Destroy(collisionInfo.gameObject);
            }
            else
            {
                FindObjectOfType<AudioManager>().PlaySound("DestroyObj1");
                FindObjectOfType<ScoreUI>().destroyPoint(scoreMultiplier);
                if (floatingTextPrefab)
                {
                    showFloatingText(collisionInfo);
                }
                Explode(collisionInfo);
                Destroy(collisionInfo.gameObject, destroyTime);
            }
        }

        if(collisionInfo.collider.tag == "Breakable")
        {
            Vector3 dir = collisionInfo.contacts[0].point - rb.transform.position;
            dir = dir.normalized;
            collisionInfo.rigidbody.AddForce(dir * 20);
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

    IEnumerator InvinciblePow(Rigidbody player)
    {
        player.transform.localScale *= sizeMultiplier;
        Invincible = true;
        yield return new WaitForSeconds(duration);
        Invincible = false;
        player.transform.localScale /= sizeMultiplier;
    }

    public void showFloatingText(Collision other)
    {
        var go = Instantiate(floatingTextPrefab, other.transform.position, Quaternion.identity, other.transform);
        int parseScore = 100 * scoreMultiplier;
        go.GetComponent<TextMesh>().text = parseScore.ToString();
    }

    void Explode(Collision i)
    {
        GameObject explosive = Instantiate(Explosion, i.transform.position, Quaternion.identity);
        explosive.GetComponent<ParticleSystem>().Play();
    }

    IEnumerator Lose()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<GameManager>().GameOver();
    }
}
