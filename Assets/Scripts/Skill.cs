using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

// Enemy가 데미지 입고 Die처리 되는 것 확인
// 문제: Enemy가 Hp가 다 닳아서 죽어야 할 상황에 안죽고 한번 더 맞아야 사라짐


//[RequireComponent(typeof(Player))]
//[RequireComponent(typeof(PlayerStats))]
public class Skill : MonoBehaviour
{
    public enum Skills { Fire, Water, Wind, Electic, Earth, End}
    [Header("Skill")]
    [SerializeField] private Skills _skillProperty;

    [SerializeField] private PlayerStats _playerStats;

    [Header("Skill Stats")]
    [SerializeField] private GameObject _skillObject;
    [SerializeField] private float _skillAttackSpeed = 0.5f;
    [SerializeField] private float _skillDamage = 10f;
    [SerializeField] private float _skillRange = 3f;
    [SerializeField] private float _skillCooldown = 5f;

    [SerializeField] private Player _player;

    [Header("Skill Attacker")]
    private EnemyScanner _enemyScanner;
    private Transform _target;
    private GameObject _attacker;
    private Vector3 _targetPosition;
    private Vector3 _targetRotation;

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
        _playerStats = GetComponent<PlayerStats>();
        _player = GetComponent<Player>();
        _enemyScanner = GetComponent<EnemyScanner>();

        // 옵저버 등록
        _playerStats.OnLevelChanged += SkillUpgrade;

        // 코루틴으로 SkillAttack을 _skillCooldown마다 반복
        StartCoroutine("RepeatSkillAttack", _skillCooldown);
    }

    private void Update()
    {
        // 투사체 이동
        if (_attacker != null)
        {
            _attacker?.transform.Translate(Vector3.forward * _skillAttackSpeed * Time.deltaTime);
        }
    }


    // 속성 자동 공격
    IEnumerator RepeatSkillAttack(float cooldownTime)
    {
        if(_target == null)
        {
            yield return null;
        }

        // 플레이어가 살아있는 경우 반복하도록 변경
        // todo: 플레이어 생존 여부 while 조건에 넣기
        while (true)
        {
            Debug.Log("=== 자동 공격 ===");
            // 공격 대상 스캔
            _target = _enemyScanner.GetRandom(SkillRange);
            
            AttackerSpawn();
            yield return new WaitForSeconds(cooldownTime);
        }
    }

    // 투사체 위치 조정 및 생성 
    // 문제점: 투사체 발사 시 대상을 향하긴 하는데 바닥을 향함
    // 방향 지정 문제로 보입니다
    private void AttackerSpawn()
    {
        _targetPosition = _target.position;
        Vector3 _posDiff = transform.position - _targetPosition;
        _targetRotation = new Vector3(_posDiff.x, 0f, _posDiff.z);

        // 투사체 프리팹 생성
        _attacker = Instantiate(_skillObject, transform.GetChild(transform.childCount - 1).position, transform.rotation);
        _attacker.transform.LookAt(_target);
    }

    private void SkillAttack()
    {
        AttackerSpawn();
        Vector3 _targetPosition = _target.position;
        Vector3 _targetDirection = (_targetPosition - transform.position).normalized;
        Debug.Log(_targetPosition);

    }

    // 속성 강화
    public void SkillUpgrade(int curLvl, int maxLvl)
    {
        // 속성 강화 로직
        // level에 따라 다른 속성 강화 메서드 호출
        // 3, 6, 9 제외한 레벨
        switch (curLvl)
        {
            case 3:
                // Slash 갯수 증가
                Debug.Log($"[SkillUpgrade] 투사체 갯수 증가");
                StartCoroutine("RepeatSkillAttack", _skillCooldown* 1.5f);
                break;
            case 6:
                // 뎀 증가
                SkillDamageUpgrade();
                Debug.Log("스킬 이펙트 변경");
                // skillObject 변경?
                break;
            case 9:
                // 새로운 스킬 생성
                break;
            default:
                SkillDamageUpgrade();
                SkillRangeUpgrade();
                //SkillCooldownUpgrade();
                break;
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
