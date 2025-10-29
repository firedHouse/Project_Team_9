using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum Skills { Fire, Water, Wind, End}
    [SerializeField] private Skills _skillProperty;
    // 플레이어 레벨 대신 사용
    [SerializeField] private int _skillLevel = 1;
    [SerializeField] private float _skillDamage = 1f;
    [SerializeField] private float _skillRange = 3f;
    [SerializeField] private float _skillCooldown = 10f;

    // [SerializeField] private GameObject _skillEffect; // 이펙트 프리팹

    public int SkillLevel
    {
        get => _skillLevel;
    }

    public float SkillDamage
    {
        get => _skillDamage;
        set => _skillDamage = value;
    }

    public float SkillRange
    {
        get => _skillRange;
        set => _skillRange = value;
    }

    public float SkillCooldown
    {
        get => _skillCooldown;
        set => _skillCooldown = value;
    }

    private void Awake()
    {
        Init();
    }
    
    private void Init()
    {
        // player property 가져오기
        // 코루틴으로 SkillAttack을 _skillCooldown마다 반복
        SkillAttack(_skillDamage);
    }

    private void Update()
    {
        // 테스트 코드
        if (Input.anyKeyDown)
        {
            InvokeRepeating("SkillLevelUp",0f,5f );
        }
    }

    // 속성 자동 공격 
    private void SkillAttack(float skillDamage)
    {
        Debug.Log($"플레이어 공격: Damage - {skillDamage} / Range - {_skillRange} / ");
        // 범위 내 Enemy에 데미지 주기
    }
    
    // 속성 레벨업
    public void SkillLevelUp()
    {
        if (_skillLevel == 10)
        {
            _skillLevel++;
        }
    }
}
