using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Nav 사용


public class Enemy : Unit
{
    //반납할 때 사용할 키 = 프리펩의 이름
    [Header("Pool Key")]
    [SerializeField] private string _prefabKey = "EnemyPrefabName";

    [SerializeField] private float _attackCooldown = 1.0f;
    [SerializeField] private float _gainExp = 500f;
    private float _lastAttackTime = 0f;


    private float _lastUpdateTime = 0f;
    private float _DestinationInterval = 3f;
    private Transform _playerTransform;
    private NavMeshAgent navMeshAgent;
 

    protected override void Awake()
    {
        base.Awake();
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = false;
        }

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }

    }
    //풀에서 가져올 때 호출하는 초기화 로직
    public void OnSpawned()
    {
        if (navMeshAgent == null)
        {
            navMeshAgent = gameObject.AddComponent<NavMeshAgent>();
        }
        // NavMeshAgent 활성화 및 이동 시작
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
            navMeshAgent.speed = moveSpeed;
            currentHp = maxHp;
        }

    }

    //거리 계산 후, 감지 거리 내 들어올 시 추적 및 이동 
    private void Update()
    {
        if (_playerTransform != null & navMeshAgent.enabled)
        {
            //경로 갱신 인터벌 추가
            if (Time.time - _lastUpdateTime >= _DestinationInterval)
            {
                navMeshAgent.SetDestination(_playerTransform.position);
                _lastUpdateTime = Time.time;
            }
         
        }
    }

    //여현구: 어떤 컴포넌트를 플레이어 오브젝트에 담을 지에 따라 GetComponent가 달라짐 유닛으로 통일
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                Unit playerUnit = collision.gameObject.GetComponent<Unit>();
                playerUnit.TakeDamage(damage);
                Debug.Log("플레이어와 충돌하여 데미지");
                _lastAttackTime = Time.time;
            }
        }
    }

    protected override void Die()
    {
        SoundManager.Instance.PlaySFX("Player_Fireball");
        Debug.Log($"{gameObject}유닛 사망");

        //NavMeshAgent 기능 정지
        if (navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
        }

        ObjectPoolManager.Instance.ReturnObject(gameObject, _prefabKey);
    }

}
