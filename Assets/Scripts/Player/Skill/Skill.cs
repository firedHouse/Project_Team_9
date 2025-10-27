using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] private string _skillProperty; // to-do : 플레이어 속성 타입으로 자료형 수정, 플레이어가 지정한 속성으로 지정
    [SerializeField] private int _skillLevel = 1;
    [SerializeField] private float _skillDamage = 1f;
    [SerializeField] private float _skillRange = 3f;
    [SerializeField] private float _skillCooldown = 10f;

    [SerializeField] private GameObject _player;

    private void Init()
    {
        // player property 가져오기
        // 코루틴으로 속성 자동 공격 _skillCooldown마다 반복
    }

    // 속성 자동 공격 
    private void SkillAttack(float skillDamage)
    {
        Debug.Log($"플레이어 공격: Damage - {skillDamage} / Range - {_skillRange} / ");
    }

    // 속성 레벨업
    private void SkillLevelUp()
    {
        _skillLevel++;
    }

    // 속성 강화
    private void SkillEnforce()
    {
        // 속성 강화 로직
        // _skillLevel에 따라 다른 속성 강화 메서드 호출

    }

    // 대미지 업그레이드
    private void SkillDmgUp()
    {
        _skillDamage++;
    }

    // 범위 업그레이드
    private void SkillRngUp()
    {
        _skillRange++;
    }

    // 쿨타임 업그레이드
    private void SkillCdUp()
    {
        _skillCooldown--;
    }
}
