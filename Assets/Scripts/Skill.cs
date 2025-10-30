using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum Skills { Fire, Water, Wind, End}
    [SerializeField] private Skills _skillProperty;
    [SerializeField] private PlayerStats playerStats;
    // 플레이어 레벨 대신 사용
    [SerializeField] private float _skillDamage = 1f;
    [SerializeField] private float _skillRange = 3f;
    [SerializeField] private float _skillCooldown = 5f;

    // [SerializeField] private GameObject _skillEffect; // 이펙트 프리팹

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
        playerStats = GetComponent<PlayerStats>();
        playerStats.OnLevelChanged += SkillUpgrade;
        // 코루틴으로 SkillAttack을 _skillCooldown마다 반복
        Debug.Log("자동공격 코루틴 실행");
        StartCoroutine("RepeatSkillAttack", _skillCooldown);
    }

    /// <summary>
    /// 속성 자동 공격 코루틴
    /// </summary>
    /// <param name="cooldownTime">자동 공격 쿨타임</param>
    /// <returns></returns>
    IEnumerator RepeatSkillAttack(float cooldownTime)
    {
        // 플레이어가 살아있는 경우 반복
        while (true)
        {
            Debug.Log($"{_skillProperty} 자동 공격: Damage - {_skillDamage} / Range - {_skillRange}");
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    // 속성 강화
    public void SkillUpgrade(int curLvl, int maxLvl)
    {
        // 속성 강화 로직
        // level에 따라 다른 속성 강화 메서드 호출
        if (curLvl % 2 == 0)
        {
            SkillDamageUpgrade();
            SkillCooldownUpgrade();
        }
        else
        {
            SkillDamageUpgrade();
            SkillRangeUpgrade();
        }

        if (curLvl == 5)
        {
            Debug.Log("스킬 이펙트 변경");
        }
    }

    // 대미지 업그레이드
    private void SkillDamageUpgrade()
    {
        Debug.Log("스킬 데미지 증가");
        _skillDamage++;
    }

    // 범위 업그레이드
    private void SkillRangeUpgrade()
    {
        Debug.Log("스킬 범위 증가");
        _skillRange += 0.5f;
    }

    // 쿨타임 업그레이드
    private void SkillCooldownUpgrade()
    {
        Debug.Log("스킬 쿨다운 감소");
        _skillCooldown -= 0.5f;
    }
}
