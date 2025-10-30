using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;
    [SerializeField] private float maxHp;
    [SerializeField] private float curruntHp;
        

    void Update()
    {
        hpBar.value = curruntHp / maxHp;
        if (curruntHp > maxHp)
        {
            curruntHp = maxHp;
        }
        if (curruntHp < 0)
        {
            curruntHp = 0;
        }
        hpText.text = curruntHp + " / " + maxHp;
    }
}
