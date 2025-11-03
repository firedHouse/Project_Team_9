using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;
    [SerializeField] private Player player;    

    private void Start() // 시작 시, Player 찾아서 적용
    {
        if (player == null)
        {
            var foundPlayer = FindObjectOfType<Player>();
            if (foundPlayer != null)
            {
                player = foundPlayer.GetComponent<Player>();
            }
            else
            {
                Debug.LogError("참조된 Player가 존재하지 않습니다.");
            }
        }

    }

    void Update()
    {
        if(player == null)
        {
                       return;
        }

        float maxHp = player.maxHp;
        float curruntHp = player.currentHp;

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
