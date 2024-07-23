using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject creditsCanvas;
    public GameObject gameCanvas, pauseCanvas;

    public void Resume()
    {
        gameCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
        Debug.Log("Resume Clicked");
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

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Debug.Log("Main Menu Clicked");
    }
}
