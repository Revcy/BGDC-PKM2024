using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Sprite[] spriteNumbers = new Sprite[10]; // fill with numbers

    public List<Image> spriteFieds; // set timer Fields
    public static GameState currentState;
    public bool isStop = false;
    public float timer;
    public void Start()
    {
        timer = 900;
    }
    public void Update()
    {
        if (currentState == GameState.Paused) isStop = !isStop;

        if (!isStop)
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                timer = 0;
                GameManager.instance.Death();
            }
            SyncTimer();
        }
    }

    public void SyncTimer()
    {
        int timeInt = Mathf.FloorToInt(timer);
        string timeStr = timeInt.ToString().PadLeft(spriteFieds.Count, '0');

        for (var i = 0; i < spriteFieds.Count; i++)
        {
            var index = timeStr[spriteFieds.Count - 1 - i] - '0';
            spriteFieds[i].sprite = spriteNumbers[index];
        }
    }
}
