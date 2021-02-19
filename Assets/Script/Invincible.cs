using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Invincible : MonoBehaviour
{
    Image invincibleBar;
    float timeLeft;
    float chargeAmt;
    
    // Start is called before the first frame update
    void Start()
    {
        invincibleBar = GetComponent<Image>();
        timeLeft = FindObjectOfType<PlayerCollision>().duration;
    }

    // Update is called once per frame
    void Update()
    {
        chargeAmt = FindObjectOfType<PlayerCollision>().addupPoint;
        if (FindObjectOfType<PlayerCollision>().Invincible)
        {
            timeLeft -= Time.deltaTime;
            invincibleBar.fillAmount = timeLeft / FindObjectOfType<PlayerCollision>().duration;
        }
        else
        {
            invincibleBar.fillAmount = chargeAmt / FindObjectOfType<PlayerCollision>().requiredPoint;
            timeLeft = FindObjectOfType<PlayerCollision>().duration;
        }
    }
}
