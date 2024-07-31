using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DontDestroyLose : MonoBehaviour
{
    public static DontDestroyLose instance;
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
