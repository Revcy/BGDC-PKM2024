using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Sprite[] spriteNumbers = new Sprite[10]; // fill with numbers

    public List<Image> spriteFieds; // set timer Fields
    public static GameState currentState;
    public bool isStop;
    public float timer = 300f;
    public void Update()
    {
        if (currentState == GameState.Paused) isStop = !isStop;

        if (!isStop)
        {
            timer -= Time.deltaTime;
            SyncTimer();
        }
    }
    public void SyncTimer()
    {
        for (var i = 0; i < spriteFieds.Count; i++)
        {
            var index = (int)(timer / Mathf.Pow(10, i) % 10);
            spriteFieds[i].sprite = spriteNumbers[index];
        }
    }
}
