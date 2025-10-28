using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private float limittimer;

    void Start()
    {
        Time.timeScale = 1;
    }   

    void Update()
    {
        if (limittimer > 0)
        {
            limittimer -= Time.deltaTime;
        }
        else
        {
            limittimer = 0;
        }
        int min = (int)(limittimer / 60);
        float sec = limittimer % 60;
        timerText.text = string.Format("{0:00}:{1:00}", min, sec);
    }

}
