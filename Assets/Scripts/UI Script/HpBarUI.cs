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

    private void Start() // 시작 시, Player 찾아서 적용
=======
    [SerializeField] private Player player;
        
    void Start()
>>>>>>> Stashed changes
    {
        StartCoroutine(FindPlayer());
    }

    private IEnumerator FindPlayer()
    {
        while (player == null) // 게임 시작할 때, Player 를 자동으로 삽입
        {
<<<<<<< Updated upstream
            var foundPlayer = FindObjectOfType<Player>();
            if (foundPlayer != null)
            {
                player = foundPlayer.GetComponent<Player>();
            }
            else
            {
                Debug.LogError("참조된 Player가 존재하지 않습니다.");
            }
=======
            player = FindObjectOfType<Player>();
            if (player != null)
            {
                yield break;
            }
            yield return new WaitForSeconds(0.2f);
>>>>>>> Stashed changes
        }

    }

    void Update()
    {
<<<<<<< Updated upstream
        if(player == null)
        {
                       return;
        }

=======
        if (player == null) // Player 없으면 일단 대기
        {
            return;
        }
>>>>>>> Stashed changes
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
