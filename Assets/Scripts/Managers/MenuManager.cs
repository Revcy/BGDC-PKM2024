using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject creditsCanvas;

    public void Play()
    {
        SceneManager.LoadScene("Level 1");
        Debug.Log("Play Clicked");
    }

    public void Options()
    {
        optionsCanvas.SetActive(true);
        Debug.Log("Options Clicked");
    }

    public void Credits()
    {
        creditsCanvas.SetActive(true);
        Debug.Log("Credits Clicked");
    }

    public void Exit()
    {
       //Application.Quit();
       Debug.Log("Exit Clicked");
    }
}
