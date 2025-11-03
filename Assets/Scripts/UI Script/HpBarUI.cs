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

        
=======
    private Player player;    


>>>>>>> Stashed changes
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
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    }

    void Update()
    {
<<<<<<< Updated upstream
        if (player == null) // Player ì—†ìœ¼ë©´ ì¼ë‹¨ ëŒ€ê¸°
=======
        if (player == null) // Player ¾øÀ¸¸é ÀÏ´Ü ´ë±â
>>>>>>> Stashed changes
        {
            return;
        }

        float maxHp = player.maxHp;
        float currHp = Mathf.Clamp(player.currentHp, 0, maxHp);

<<<<<<< Updated upstream
=======
        hpBar.value = currHp / maxHp;
        hpText.text = $"{currHp} / {maxHp}";
>>>>>>> Stashed changes
    }
}

