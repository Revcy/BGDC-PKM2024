using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DontDestroyWin : MonoBehaviour
{
    public static DontDestroyWin instance;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        gameObject.SetActive(false); // Deactivate the GameObject

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
        gameObject.SetActive(false);
    }
}
