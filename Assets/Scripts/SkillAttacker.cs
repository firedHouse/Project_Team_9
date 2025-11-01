
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Skill))]
public class SkillAttacker : MonoBehaviour
{
    [SerializeField] private Skill _skill;
    [SerializeField] private float _skillDamage;


    private void Awake()
    {
        _skill = GetComponent<Skill>();
        Init();
    }

    private void Init()
    {
        // 투사체 속성 설정
        _skillDamage = _skill.SkillDamage;
        Debug.Log("[SkillAttacker] 생성");
    }


    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"[SkillAttacker] 투사체 충돌({other.tag})");
        //Debug.Log(other.tag);
        Debug.Log($"[SkillAttacker] 공격 대상 : {other.gameObject.name}");
        
        // Enemy 충돌시
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"[SkillAttacker] {_skillDamage}");
            // 충돌 Enemy가 takeDamage
            Enemy enemyUnit = other.gameObject.GetComponent<Enemy>();
            enemyUnit?.TakeDamage(_skillDamage);
            
            Destroy(gameObject);
            Debug.Log("[SkillAttacker] 투사체 소멸");
        }
    }
}
