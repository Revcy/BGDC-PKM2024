using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource musicSource, sfxSource;
    public Slider musicSlider, sfxSlider;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadVolume();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize the sliders' values and add listeners
        if (musicSlider != null)
        {
            musicSlider.value = musicSource.volume;
            musicSlider.onValueChanged.AddListener(delegate { SetMusicVolume(musicSlider.value); });
        }

        if (sfxSlider != null)
        {
            sfxSlider.value = sfxSource.volume;
            sfxSlider.onValueChanged.AddListener(delegate { SetSFXVolume(sfxSlider.value); });
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
        musicSource.Play();
    }

    public void SetMusicVolume(float volume)
    {
        musicSource.volume = volume;
        PlayerPrefs.SetFloat("MusicVolume", volume);
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicSource.volume = PlayerPrefs.GetFloat("MusicVolume");
        }
        else
        {
            musicSource.volume = 1f; // Default volume
        }

        if (PlayerPrefs.HasKey("SFXVolume"))
        {
            sfxSource.volume = PlayerPrefs.GetFloat("SFXVolume");
        }
        else
        {
            sfxSource.volume = 1f; // Default volume
        }
    }
}
