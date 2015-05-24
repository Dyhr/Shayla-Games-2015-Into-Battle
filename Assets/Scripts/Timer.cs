using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private int minutes;
    private float seconds;
    private bool started;

    public static Timer instance;

    public void Awake()
    {
        instance = this;
    }
    
    public void FixedUpdate()
    {
        if (started)
        {
            seconds += Time.fixedDeltaTime;
            while (seconds >= 60)
            {
                seconds -= 60;
                minutes++;
            }
        }
        GetComponent<Text>().text = (minutes < 10 ? "0" : "") + minutes + ":" + (seconds < 10 ? "0" : "") + ((int)seconds);
    }

    public void StartTimer()
    {
        started = true;
    }
    public void StopTimer()
    {
        started = false;
    }
}
