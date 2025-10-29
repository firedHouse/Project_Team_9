using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int _playerMaxLevel = 10;
    [SerializeField] private float _playerMaxExp = 1000;
    [SerializeField] private float _exp = 200;
    [SerializeField] private float _expIncreaseAmount = 700;
    private int _playerCurrentLevel;
    private float _playerCurrentExp;
    // 플레이어 체력, 경험치, 이속(변수 이미 존재)

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _playerCurrentLevel = 1;
        _playerCurrentExp = 0;
    }

    private void Update()
    {
        // 테스트 코드
        //if (Input.anyKeyDown)
        //{
        //    GetExp(_exp);
        //    Debug.Log($"현재 레벨 : {_playerCurrentLevel} | {_playerCurrentExp} / {_playerMaxExp}");
        //}
    }

    public void GetExp(float exp)
    {
        if(_playerCurrentLevel == _playerMaxLevel)
        {
            return;
        }
        // 경험치 업
        _playerCurrentExp += exp;
        // 경험치가 최대를 넘어가면 레벨업 + 경험치 초기화
        // 들어온 경험치를 더했을 때 최대 경험치보다 클 경우 초기화 후 최대 경험치 - 현재 경험치 한 값을 더해준다
        if(_playerCurrentExp >= _playerMaxExp)
        {
            float exceededExp = _playerCurrentExp - _playerMaxExp;
            LevelUp();
            _playerCurrentExp += exceededExp;
        }
    }

    public void LevelUp()
    {
        // 현재 최대 레벨이라면 제한
        if (_playerCurrentLevel == _playerMaxLevel)
        {
            return;
        }
        _playerCurrentLevel++;
        _playerCurrentExp = 0;
        _playerMaxExp += _expIncreaseAmount;
    }
}
