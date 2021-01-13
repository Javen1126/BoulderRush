using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    bool destroyObj = false;
    public Transform player;
    public Text scoreText;

    void Start()
    {
        scoreText.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        //scoreText.text = player.position.z.ToString("0");
    }

    public void resetDest()
    {
        if (destroyObj == true)
        {
            destroyObj = false;
        }
    }

    public void destroyPoint()
    {
        if (destroyObj == false)
        {
            destroyObj = true;
            int score = int.Parse(scoreText.text) + 100;
            scoreText.text = score.ToString();
            Debug.Log("trying to add score");
        }
        resetDest();
    }
}
