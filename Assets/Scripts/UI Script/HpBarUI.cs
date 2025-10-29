using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBarUI : MonoBehaviour
{
    [SerializeField] public Player player;
    [SerializeField] public Slider hpBar;
    [SerializeField] public float maxHp;
    [SerializeField] public float currentHp;

    void Update()
    {
            // transform.position = player.transform.position;
            // hpBar.value = currentHp / maxHp;
    }
}
