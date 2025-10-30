using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;
    [SerializeField] private Player player;
    private float maxHp;
    private float curruntHp;
        
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("참조된 Player가 존재하지 않습니다.");
        }
    }

    void Update()
    {
        maxHp = player.maxHp;
        curruntHp = player.currentHp;

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
