using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject optionsCanvas;
    public GameObject creditsCanvas;
    public GameObject gameCanvas, pauseCanvas;
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject journalCanvas;
    public bool isJournalActive = false;
    public void Resume()
    {
        GameManager.instance.Resume();
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
    public void IngameResume()
    {
        GameManager.instance.Resume();
        creditsCanvas.SetActive(false);
        optionsCanvas.SetActive(false);
        pauseCanvas.SetActive(false);
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        Debug.Log("Resume Clicked");
    }
    public void IngamePause()
    {
        GameManager.instance.Pause();
        pauseCanvas.SetActive(true);
        resumeButton.SetActive(true);
        pauseButton.SetActive(false);
        Debug.Log("Pause Clicked");
    }
    public void Journaling()
    {
        GameManager.instance.Journaling();
        if (isJournalActive)
        {
            journalCanvas.SetActive(false);
            isJournalActive = false;
        }
        else{
            journalCanvas.SetActive(true);
            isJournalActive = true;
        }

    }
}
