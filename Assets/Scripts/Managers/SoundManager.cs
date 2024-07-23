using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource bgmSource;
    [SerializeField] AudioSource sfxSource;
    [Header("Audio Asset")]
    public AudioClip doorOpen;
    public AudioClip buttonClick;
    public AudioClip extinguishShoot;
    public AudioClip extinguishedFire;
    public AudioClip fireSound;
    public AudioClip movementSound;
    public AudioClip bgMusic;

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
}
