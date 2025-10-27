using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    private Transform _playerTransform;
    private NavMeshAgent navMeshAgent;
    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }
    }

    //거리 계산 후, 감지 거리 내 들어올 시 추적 및 이동 
    private void Update() 
    {
        if (_playerTransform != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
            if (distanceToPlayer <= targetRange)//필드명 미정
            {
                ChasePlayer();
            }
        }
    }

    // 목표 방향 계산 및 추적하는 메서드. 
    // 방향, 이동, 회전 
    private void ChasePlayer()
    {
        Vector3 direction = (_playerTransform.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.identity;
    }

    protected override void Die()
    {
        // 사망 로직 구현
        // 이펙트 적으로 구현하기 쉬운 방향으로 설계
        Debug.Log($"{gameObject}유닛 사망");
    }

}
