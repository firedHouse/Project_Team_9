using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum Skills { Fire, Water, Electic, End }
    [SerializeField] private PlayerStats _playerStats;

    [Header("Skill Stats")]
    [SerializeField] private Skills _skillProperty;
    [SerializeField] private GameObject _skillObject;
    [SerializeField] private float _skillAttackSpeed = 0.5f;
    [SerializeField] private float _skillDamage = 10f;
    [SerializeField] private float _skillRange = 3f;
    [SerializeField] private float _skillCooldown = 5f;

    [Header("Skill Attacker")]
    [SerializeField] private EnemyScanner _enemyScanner;
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _attacker;
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

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _enemyScanner = FindObjectOfType<EnemyScanner>();
        if (_enemyScanner == null)
        {
            Debug.Log("에너미 스캐너 초기화 안됨");
            _enemyScanner = new EnemyScanner();
        }
        // 코루틴으로 SkillAttack을 _skillCooldown마다 반복
        Debug.Log("코루틴 실행");
        StartCoroutine("RepeatSkillAttack", _skillCooldown);
    }
    
    private void Init()
    {
        // 옵저버 등록
        _playerStats.OnLevelChanged += SkillUpgrade;

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
            Debug.Log("타겟 없음");
            _target = _enemyScanner.GetRandom(SkillRange);
            yield return new WaitForSeconds(cooldownTime);
        }

        // 플레이어가 살아있는 경우 반복하도록 변경
        // todo: 플레이어 생존 여부 while 조건에 넣기
        while (true)
        {
            Debug.Log("=== 자동 공격 ===");
            // 공격 대상 스캔
            _target = _enemyScanner.GetRandom(SkillRange);
            if(_target == null)
            {
                Debug.Log("타겟 없음");
                _target = _enemyScanner.GetRandom(SkillRange);
                yield return new WaitForSeconds(cooldownTime);
                continue;
            }
            Debug.Log("타겟이 존재");
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

        // 투사체 프리팹 가져오기
        //GameObject attacker = ObjectPoolManager.Instance.GetObject(_skillObject.name);
        //Vector3 attackerPosition = transform.position + _targetRotation * 0.5f;
        //attacker.SetActive(true);

        _attacker = Instantiate(_skillObject, transform.GetChild(transform.childCount - 1).position, transform.rotation);
        //_attacker.transform.LookAt(_target);
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
                //StartCoroutine("RepeatSkillAttack", _skillCooldown* 1.5f);
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
