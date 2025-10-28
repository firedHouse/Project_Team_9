using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI; //Nav 사용

//NavMesh 강제
[RequireComponent(typeof(NavMeshAgent))] 
public class Enemy : Unit
{
    // 어택 세팅 필요
    [SerializeField] private float attackDamage = 10f; 

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
            
            navMeshAgent.SetDestination(_playerTransform.position);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //여현구: 어떤 컴포넌트를 플레이어 오브젝트에 담을 지에 따라 GetComponent가 달라짐. Player, Unit, Job 등등
            Unit playerUnit = collision.gameObject.GetComponent<Unit>(); 
            playerUnit.TakeDamage(attackDamage);

            Debug.Log("플레이어와 충돌하여 데미지");

        }
    }

    protected override void Die()
    {
        // 사망 로직 구현
        // 이펙트 적으로 구현하기 쉬운 방향으로 설계
        Debug.Log($"{gameObject}유닛 사망");
    }

}
