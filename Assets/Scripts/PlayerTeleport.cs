using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    private GameObject currentTeleporter;
    public GameObject WinCanvas;
    private bool isWin = false;
    public SoundManager soundManager;

    private void Awake()
    {
        soundManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<SoundManager>();
    }

    private void Start()
    {
        WinCanvas = DontDestroyWin.instance.gameObject;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            if (isWin == true)
            {
                soundManager.PlayWinSound(soundManager.winSFX);
                WinCanvas.SetActive(true);

            }
            if (currentTeleporter != null)
            {
                soundManager.PlaySFX(soundManager.doorOpen);
                transform.position = currentTeleporter.GetComponent<RoomDoor>().GetDestination().position;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            currentTeleporter = collision.gameObject;
        }
        else if (collision.CompareTag("WinDoor")){
            isWin = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Door"))
        {
            if(collision.gameObject == currentTeleporter)
            {
                currentTeleporter = null;
                isWin = false;
            }
        }
    }
}
