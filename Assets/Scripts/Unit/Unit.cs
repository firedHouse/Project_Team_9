using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] public float maxHp;
    [SerializeField] public float damage;
    [SerializeField] protected float moveSpeed;

    public float currentHp;

    // moveSpeed는 외부에서 읽기만 가능하도록 프로퍼티 부여
    public float MoveSpeed => moveSpeed; 

    protected virtual void Awake()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float amount)
    {
        currentHp -= amount;

        if (currentHp <= 0)
        {
            Die();

        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{gameObject.name} died.");
        //에너미 오브젝트 풀링
        //플레이어 Die 시 모르겠다?
    }
}