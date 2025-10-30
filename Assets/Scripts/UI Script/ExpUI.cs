using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpUI : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Slider expBar;
    [SerializeField] private Text levelText;
    float _maxExp;
    float _currentExp;
    float _currentLevel;
    float _maxLevel;

    private void Start()
    {
        if (playerStats == null)
        {
            playerStats = GetComponent<PlayerStats>();
            if (playerStats == null)
            {
                Debug.LogError("참조된 PlayerStats가 존재하지 않습니다.");
                return;
            }
        }

        playerStats.OnExpChanged += UpdateExpUI;
        playerStats.OnLevelChanged += UpdateLevelUI;

    }

    private void UpdateExpUI(float currentExp, float maxExp)
    {
        _currentExp = currentExp;
        _maxExp = maxExp;
        expBar.value = currentExp / maxExp;
    }

    private void UpdateLevelUI(int currentLevel, int maxLevel)
    {
        _currentLevel = currentLevel;
        _maxLevel = maxLevel;
        levelText.text = $"Lv. {_currentLevel}";
        if (_currentLevel >= _maxLevel)
        {
            levelText.text = $"Level. Max";
        }
    }
}
