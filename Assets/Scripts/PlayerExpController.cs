using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExpController : MonoBehaviour
{
    [SerializeField] private PlayerStats playerStats;
    private float _currentExp;
    private float _maxExp;
    private float _currentLvl;
    private float _maxLvl;


    void Start()
    {
        playerStats = GetComponent<PlayerStats>();
        // playerStats.OnExpChanged += UpdateExpBarTest;
        // playerStats.OnLevelChanged += UpdateLvlTest;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void UpdateExpBarTest(float curExp, float maxExp)
    {
        _currentExp = curExp;
        _maxExp = maxExp;
        Debug.Log($"경험치 업데이트 - {_currentExp} / {_maxExp}");

    }
    private void UpdateLvlTest(int curLvl, int maxLvl)
    {
        _currentLvl = curLvl;
        _maxLvl = maxLvl;
        Debug.Log($"레벨 업데이트 - {_currentLvl} / {_maxLvl}");

    }
}

