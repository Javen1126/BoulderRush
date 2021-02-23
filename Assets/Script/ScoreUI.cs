using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    // Start is called before the first frame update
    bool destroyObj = false;
    public Text scoreText;
    public Text highscoreText;

    void Start()
    {
        scoreText.text = "0";
        highscoreText.text = "High Score : " + FindObjectOfType<ScoreManager>().highscore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public void destroyPoint(int i)
    {
        if (destroyObj == false)
        {
            destroyObj = true;
            int score = int.Parse(scoreText.text) + 100*i;
            scoreText.text = score.ToString();
            ScoreManager.instance.UpdateScore(scoreText.text);
            ScoreManager.instance.UpdateHighScore();
            highscoreText.text = "High Score : " + FindObjectOfType<ScoreManager>().highscore.ToString();
            Debug.Log("trying to add score");
        }
        resetDest();
    }
}
