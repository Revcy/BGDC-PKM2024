using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject creditsCanvas;
    public GameObject mainMenuCanvas;
    public GameObject levelCanvas;

    public void Play()
    {
        levelCanvas.SetActive(true);
        Debug.Log("Play Clicked");
    }
    public void PlayTutorial()
    {
        SceneManager.LoadScene("Tutorial");
        Debug.Log("Tutorial Clicked");
    }
    public void PlayMain()
    {
        SceneManager.LoadScene("Level 1");
        levelCanvas.SetActive(true);
        Debug.Log("Main Clicked");
    }

    public void Options()
    {
        optionsCanvas.SetActive(true);
        mainMenuCanvas.SetActive(false);
        Debug.Log("Options Clicked");
    }
    public void OptionsClose()
    {
        optionsCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
        Debug.Log("Options Closed");
    }
    public void Credits()
    {
        creditsCanvas.SetActive(true);
        Debug.Log("Credits Clicked");
    }

    public void CreditsClose()
    {
        creditsCanvas.SetActive(false);
        Debug.Log("Credits Closed");
    }
    public void Exit()
    {
       Application.Quit();
       Debug.Log("Exit Clicked");
    }
}