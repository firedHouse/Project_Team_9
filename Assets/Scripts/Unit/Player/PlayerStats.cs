using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Exp Management")]
    [Tooltip("PlayScene에 경험치 증가를 관측하여 필수로 존재해야 할 오브젝트 입니다")]
    [SerializeField] private int _playerMaxLevel = 9;
    [SerializeField] private float _playerMaxExp = 1000;
    [SerializeField] private float _expIncreaseAmount = 700;
    //float _exp = 500f;
    private int _playerCurrentLevel = 1;
    private float _playerCurrentExp = 0;
    // 레벨업시 증가할 스텟량
    private float _addHp = 20f;
    private float _addDamage = 0.5f;

    public event Action<float, float> OnExpChanged;
    public event Action<int, int> OnLevelChanged;

    public int PlayerCurrentLevel 
    {
        get => _playerCurrentLevel;
    }

    private void Awake()
    {
    }

    private void Update()
    {
        

        // 테스트 코드
        //if (Input.GetKeyDown(KeyCode.A))
        //{
        //    GetExp(_exp);
        //    Debug.Log($"현재 레벨 : {_playerCurrentLevel} | {_playerCurrentExp} / {_playerMaxExp}");
        //}
    }

    // 추후 이동 가능
    public void GetExp(float exp)
    {
        // 최대 레벨 도달시 리턴
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
            // 넘친 경험치 임시로 저장 후 초기화된 경험치에 더하기
            float exceededExp = _playerCurrentExp - _playerMaxExp;
            LevelUp();
            _playerCurrentExp += exceededExp;
        }
        OnExpChanged?.Invoke(_playerCurrentExp, _playerMaxExp);
    }

    // 추후 이동 가능
    public void LevelUp()
    {
        // 현재 최대 레벨이라면 제한
        if (_playerCurrentLevel == _playerMaxLevel)
        {
            return;
        }
        _playerCurrentLevel++;
        _playerMaxExp += _expIncreaseAmount;
        _playerCurrentExp = 0;
        
        OnLevelChanged?.Invoke(_playerCurrentLevel, _playerMaxLevel);

        //레벨업시 스텟 증가
        Player player = FindObjectOfType<Player>();

        player.LevelUpStats(_addHp, _addDamage);
    }
}
