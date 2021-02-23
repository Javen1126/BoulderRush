using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreMultiplierUI : MonoBehaviour
{
    public Text scoreMultiplierText;

    void Start()
    {
        scoreMultiplierText.text = "0";
    }
    private void Update()
    {
        int scoreMultiplier = FindObjectOfType<PlayerCollision>().scoreMultiplier;
        scoreMultiplierText.text = "x" + scoreMultiplier.ToString();
    }
}
