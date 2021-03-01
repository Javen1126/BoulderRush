using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject page1;
    public GameObject page2;
    public GameObject page3;
    public GameObject page4;
    public GameObject nextButton;
    public GameObject backButton;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void backPressed()
    {
        if(page4.activeSelf == true)
        {
            page4.SetActive(false);
            page3.SetActive(true);
        } 
        else if (page3.activeSelf == true)
        {
            page3.SetActive(false);
            page2.SetActive(true);
        }
        else if (page2.activeSelf == true)
        {
            page2.SetActive(false);
            page1.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
        }
    }

    public void nextPressed()
    {
        if (page1.activeSelf == true)
        {
            page1.SetActive(false);
            page2.SetActive(true);
        }
        else if (page2.activeSelf == true)
        {
            page2.SetActive(false);
            page3.SetActive(true);
        }
        else if (page3.activeSelf == true)
        {
            page3.SetActive(false);
            page4.SetActive(true);
        }
        else
        {
            tutorial.SetActive(false);
            page4.SetActive(false);
            page1.SetActive(true);
        }
    }
}
