using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] public AudioSource bgmSource;
    [SerializeField] public AudioSource sfxSource;
    [Header("Audio Asset")]
    public AudioClip doorOpen;
    public AudioClip buttonClick;
    public AudioClip extinguishShoot;
    public AudioClip extinguishedFire;
    public AudioClip fireSound;
    public AudioClip movementSound;
    public AudioClip bgMusic;
    public AudioClip winSFX;
    public AudioClip loseSFX;

    private void Start()
    {
        bgmSource.clip = bgMusic;
        bgmSource.loop = true;
        bgmSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayWinSound(AudioClip clip)
    {
        bgmSource.Stop();
        sfxSource.PlayOneShot(clip);
    }

    public void PlaySFXLoop(AudioClip clip)
    {
        if (sfxSource.clip != clip)
        {
            sfxSource.clip = clip;
            sfxSource.loop = true;
            sfxSource.Play();
        }
        else if (!sfxSource.isPlaying)
        {
            sfxSource.Play();
        }
    }

    public void StopSFXLoop()
    {
        if (sfxSource.isPlaying)
        {
            sfxSource.Stop();
            sfxSource.clip = null;
        }
    }
}