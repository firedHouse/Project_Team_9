
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SkillAttacker : MonoBehaviour
{
    [SerializeField] private Skill _skill;
    [SerializeField] private float _skillDamage;


    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _skill = FindObjectOfType<Skill>();
        // 투사체 속성 설정
        _skillDamage = _skill.SkillDamage;
        Debug.Log("[SkillAttacker] 생성");
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"[SkillAttacker] 투사체 충돌({other.tag})");
        Debug.Log(other.tag);
        Debug.Log($"[SkillAttacker] 공격 대상 : {other.gameObject.name}");
        
        // Enemy 충돌시
        if (other.CompareTag("Enemy"))
        {
            Debug.Log($"[SkillAttacker] {_skillDamage} 데미지");
            // 충돌 Enemy가 takeDamage
            // todo : takeDamage 부분에서 딜레이 주는 식으로 해결해보기
            Enemy enemyUnit = other.gameObject.GetComponent<Enemy>();
            enemyUnit?.TakeDamage(_skillDamage);
            
            Destroy(gameObject);
            Debug.Log($"[SkillAttacker] {other.gameObject.name}과 충돌로 투사체 소멸");
        }
        else if (other.CompareTag("UI"))
        {

            Debug.Log($"[SkillAttacker] 스테이지 충돌로 투사체 소멸");
            Destroy(gameObject, 0f);
        }
    }
}
