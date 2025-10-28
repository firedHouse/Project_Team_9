using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Skill))]
public class SkillController : MonoBehaviour
{
    // 1. Skill 관리 2. 특성 강화
    [SerializeField] public Skill _skill;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _skill = GetComponent<Skill>();
    }

    private void Update()
    {
        // SkillUpgrade(); // 테스트 코드
    }

    // 속성 강화
    public void SkillUpgrade()
    {
        // 속성 강화 로직
        // _skillLevel에 따라 다른 속성 강화 메서드 호출
        if (_skill.SkillLevel % 2 == 0)
        {
            SkillDamageUpgrade();
            SkillCooldownUpgrade();
        }
        else
        {
            SkillDamageUpgrade();
            SkillRangeUpgrade();
        }

        if (_skill.SkillLevel == 5)
        {
            Debug.Log("스킬 이펙트 변경");
        }
    }

    // 대미지 업그레이드
    private void SkillDamageUpgrade()
    {
        Debug.Log("스킬 데미지 1 증가");
        _skill.SkillDamage++;
    }

    // 범위 업그레이드
    private void SkillRangeUpgrade()
    {
        Debug.Log("스킬 범위 1 증가");
        _skill.SkillRange += 0.5f;
    }

    // 쿨타임 업그레이드
    private void SkillCooldownUpgrade()
    {
        Debug.Log("스킬 쿨다운 1 감소");
        _skill.SkillCooldown -= 0.5f;
    }
}
