using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Runtime.CompilerServices;
public enum GameState { MainMenu, Playing, Dead, Paused, Loading, PopupOpen }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public static GameState currentState;
    public float backToMenuDelay;
    public bool isJournaling = false;
    public GameObject LoseScreen;
    public static event Action OnBackToMenu;
    [SerializeField] private SoundManager soundManager;

    private void Awake()
    {
        InitializeSound();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;

        DontDestroyOnLoad(gameObject);
        if (SceneManager.GetActiveScene().buildIndex != 0)
            SceneManager.LoadScene(0);
    }

    private void Start()
    {
        currentState = GameState.MainMenu;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentState != GameState.MainMenu && currentState != GameState.PopupOpen && currentState != GameState.Loading)
        {
            if (currentState == GameState.Paused)
                Resume();
            else if (currentState == GameState.Playing)
                Pause();
        }
    }

    public IEnumerator BackToMenu()
    {
        Time.timeScale = 1f;
        OnBackToMenu?.Invoke();
        currentState = GameState.Loading;
        Debug.Log("BackToMenu");

        yield return new WaitForSeconds(backToMenuDelay);
        SceneManager.LoadScene(0);

        //WinCanvasManager.Instance.SetWinScreen(false);
        //SoundManager.instance.StopAllSounds();
        //DeathScreen.instance.SetDeathTrue(false);
        currentState = GameState.MainMenu;
    }

    public void Pause()
    {
        //SoundManager.instance.PauseAllSounds(true);
        //PauseMenu.instance.SetPauseActive(true);
        currentState = GameState.Paused;
        Time.timeScale = 0f;
    }
    public void Journaling()
    {
        isJournaling = !isJournaling;
        if (isJournaling) Pause();
        else Resume();
    }
    public void Resume()
    {
        //SoundManager.instance.PauseAllSounds(false);
        //PauseMenu.instance.SetPauseActive(false);
        currentState = GameState.Playing;
        Time.timeScale = 1;
    }

    public void Death()
    {
        if (currentState == GameState.Dead) return;

        Time.timeScale = 0;
        currentState = GameState.Dead;
        Debug.Log("Player ran out of time");
        soundManager.PlayWinSound(soundManager.loseSFX);
        LoseScreen.SetActive(true);
    }

    [ContextMenu("Remove All Player Prefs")]
    public void RemoveAllPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void InitializeSound()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }
}



