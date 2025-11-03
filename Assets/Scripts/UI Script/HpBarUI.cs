using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;



    private Player player;    



    void Start()
    {
        StartCoroutine(WaitForPlayer());
    }

    private IEnumerator WaitForPlayer()
    {
        while (Player.Instance == null)
        {
            yield return new WaitForSeconds(0.1f);
        }

        player = Player.Instance;
    }

    void Update()
    {
        if (player == null) // Player 없으면 일단 대기

        {
            return;
        }

        float maxHp = player.maxHp;
        float currHp = Mathf.Clamp(player.currentHp, 0, maxHp);


        hpBar.value = currHp / maxHp;
        hpText.text = $"{currHp} / {maxHp}";

    }
}

