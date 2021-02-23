using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    bool gamePaused = false;
    public GameObject gameoverPanel;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject muteButton;
    public GameObject unmuteButton;
    public float restartDelay = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        if (gameEnded == false)
        { 
            gameEnded = true;
            Time.timeScale = 0;
            Debug.Log("Game Over!");
            gameoverPanel.SetActive(true);
            //Invoke("Restart", restartDelay);
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }

    public void PauseGame()
    {
        if(gamePaused == false)
        {
            gamePaused = true;
            pauseButton.SetActive(false);
            Time.timeScale = 0;
            Debug.Log("Game Paused");
            pausePanel.SetActive(true);
        }
    }
    public void ResumeGame()
    {
        if (gamePaused == true)
        {
            gamePaused = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1;
            Debug.Log("Game Resumed");
            pauseButton.SetActive(true);
        }
    }

    public void ToggleSound()
    {
        if (AudioListener.volume == 0f)
        {
            AudioListener.volume = 1f;
            muteButton.SetActive(true);
            unmuteButton.SetActive(false);
        }
        else
        {
            AudioListener.volume = 0f;
            muteButton.SetActive(false);
            unmuteButton.SetActive(true);
        }
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
