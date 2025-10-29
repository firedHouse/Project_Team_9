using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private Slider ExpBar;
    [SerializeField] private Text LevelText;
    [SerializeField] private float maxExp;
    [SerializeField] private float currentExp;
    private int level = 1;

    void Update()
    {
        ExpBar.value = currentExp / maxExp;
        LevelUp();
        LevelText.text = "Level : " + level;
        if(level >= 10)
        {
            level = 10;
            LevelText.text = "Level : MAX";
            currentExp = 0;
            ExpBar.value = 1;
        }

    }
    public void LevelUp()
    {
        if (currentExp >= maxExp)
        {
            currentExp = currentExp - maxExp;
            maxExp *= 1.2f;
            level++;
        }
    }
}
