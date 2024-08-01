using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAudio : MonoBehaviour
{
    public Transform center;
    public float maxVolumeDistance = 5f;
    public float minVolumeDistance = 20f;

    private AudioSource sfxSource;
    private SoundManager soundManager;
    private Transform cameraTransform;
    private float globalVolume = 1f;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();

        sfxSource = center.GetComponent<AudioSource>();
        if (sfxSource == null)
        {
            sfxSource = center.gameObject.AddComponent<AudioSource>();
        }

        if (soundManager != null && soundManager.fireSound != null)
        {
            sfxSource.clip = soundManager.fireSound;
            sfxSource.loop = true;
        }

        sfxSource.Play();

        cameraTransform = Camera.main.transform;

        LoadGlobalVolume();
    }

    private void Update()
    {
        AdjustVolumeBasedOnDistance();
    }

    private void LoadGlobalVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            globalVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        else
        {
            globalVolume = 1f;
        }
    }

    private void AdjustVolumeBasedOnDistance()
    {
        if (cameraTransform == null || sfxSource == null) return;

        float distance = Vector2.Distance(cameraTransform.position, center.position);
        
        float volume = 0f;

        if (distance < maxVolumeDistance)
        {
            volume = globalVolume; // Apply global volume setting
        }
        else if (distance > minVolumeDistance)
        {
            volume = 0f;
        }
        else
        {
            volume = globalVolume * (1 - ((distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance)));
        }

        sfxSource.volume = volume;

        if (globalVolume <= 0f)
        {
            sfxSource.volume = 0f;
            sfxSource.Stop();
        }
    }
}
