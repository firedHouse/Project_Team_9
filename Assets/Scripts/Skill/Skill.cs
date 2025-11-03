using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum Skills { Fire, Ice, End }
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private GameObject[] SkillEffects;

    [Header("Skill Stats")]
    [SerializeField] private Skills _skillProperty;
    private GameObject _skillObject;
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

    private void Awake()
    {
        
    }

    private void Start()
    {
        _playerStats = FindObjectOfType<PlayerStats>();
        _enemyScanner = FindObjectOfType<EnemyScanner>();
        Init();

        // _skillObject 생성 시 속성(property) 따라 다른 _skillObject 설정
        int propertyInt = PlayerPrefs.GetInt("Property");
        _skillProperty = (Skills)propertyInt;
        //Debug.Log(propertyStr);

        // 속성에 따라 스킬 오브젝트 지정
        _skillObject = SkillEffects[(int)_skillProperty];

        // 코루틴으로 SkillAttack을 _skillCooldown마다 반복
        Debug.Log("코루틴 실행");
        StartCoroutine("RepeatSkillAttack", _skillCooldown);
    }

    private void Init()
    {
        // 옵저버 등록
        _playerStats.OnLevelChanged += SkillUpgrade;

    }

    // 속성 자동 공격
    IEnumerator RepeatSkillAttack(float cooldownTime)
    {
        if (_target == null)
        {
            Debug.Log("[Skill] 타겟 없음");
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
            if (_target == null)
            {
                Debug.Log("[Skill] 타겟 없음");
                _target = _enemyScanner.GetRandom(SkillRange);
                yield return new WaitForSeconds(cooldownTime);
                continue;
            }

            Debug.Log("[Skill] 타겟이 존재");
            AttackerSpawn();

            // 스킬 오브젝트 생성 확인
            if (_attacker != null)
            {
                SkillSound();
                SkillAttack();
                DestroyAttacker();
            }

            yield return new WaitForSeconds(cooldownTime);
        }
    }

    // 공격 주체 위치 조정 및 생성 
    private void AttackerSpawn()
    {
        _targetPosition = _target.position;
        _attacker = Instantiate(_skillObject, _targetPosition, _target.transform.rotation);
    }

    // 공격 주체가 공격하여 타겟에게 데미지
    private void SkillAttack()
    {
        // 데미지 처리
        Enemy enemyUnit = _target.gameObject.GetComponent<Enemy>();
        enemyUnit?.TakeDamage(_skillDamage);
    }

    // 공격 주체 소멸
    private void DestroyAttacker()
    {
        Destroy(_attacker, 0.5f);
    }

    public void SkillSound()
    {
        switch (_skillProperty)
        {
            case Skills.Fire:
                SoundManager.Instance.PlaySFX("fireMagic");
                break;
            case Skills.Ice:
                SoundManager.Instance.PlaySFX("iceSlash");
                break;
        }
    }

    // 속성 강화
    public void SkillUpgrade(int curLvl, int maxLvl)
    {
        if (curLvl < maxLvl)
        {
            SkillDamageUpgrade();
            SkillRangeUpgrade();
        }
    }

    // 대미지 업그레이드
    private void SkillDamageUpgrade()
    {
        Debug.Log("스킬 데미지 증가");
        _skillDamage++;
    }

    // 스캔 범위 업그레이드
    private void SkillRangeUpgrade()
    {
        Debug.Log("스킬 범위 증가");
        _skillRange += 0.5f;
    }
}
