using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] private Slider hpBar;
    [SerializeField] private Text hpText;
<<<<<<< Updated upstream
    [SerializeField] private Player player;
    private float maxHp;
    private float curruntHp;
        
    void Start()
    {
        if (player == null)
        {
            Debug.LogError("참조된 Player가 존재하지 않습니다.");
        }
=======

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
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        maxHp = player.maxHp;
        curruntHp = player.currentHp;
=======
        if (player == null) // Player 없으면 일단 대기
        {
            return;
        }

        float maxHp = player.maxHp;
        float currHp = Mathf.Clamp(player.currentHp, 0, maxHp);
>>>>>>> Stashed changes

        hpBar.value = currHp / maxHp;
        hpText.text = $"{currHp} / {maxHp}";
    }
}
