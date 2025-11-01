using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; //Nav 사용


public class Enemy : Unit
{
    //보상 프리펩
    [Header("Reward Prefab")]
    [SerializeField] private GameObject _rewardPrefab;


    //반납할 때 사용할 키 = 프리펩의 이름
    [Header("Pool Key")]
    [SerializeField] private string _prefabKey = "EnemyPrefabName";
    
    

    [SerializeField] private float _attackCooldown = 1.0f;
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
    public override void TakeDamage(float damage)
    {      
        currentHp -= damage;
        GameObject DmgTxtClone = Instantiate(Resources.Load<GameObject>("DamageText"), transform.position + Vector3.up * 1.5f, Quaternion.identity);
        DmgTxtClone.GetComponent<DmgTxt>().DisplayDamage(damage);
        Debug.Log($"Enemy가 {damage} 데미지를 받았습니다. 남은 체력: {currentHp}");

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    protected override void Die()
    {
        //SoundManager.Instance.PlaySFX("Player_Fireball");
        Debug.Log($"{gameObject}유닛 사망");

        //NavMeshAgent 기능 정지
        if (navMeshAgent.enabled)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.enabled = false;
        }
        ObjectPoolManager.Instance.ReturnObject(gameObject, _prefabKey);
        //반납 후 리워드 스폰
        SpawnReward();

    }

    private void SpawnReward()
    {
        GameObject rewardPrefab = _rewardPrefab;
        if (rewardPrefab == null)
        {
            return;
        }
        //리워드 가져오기
        GameObject reward = ObjectPoolManager.Instance.GetObject(rewardPrefab.name);
        if (reward == null)
        {
            return;
        }
        //몬스터 위치에서 약간 위에 생성하고 True
        Vector3 spawnPosition = transform.position + Vector3.up * 0.5f;
        reward.transform.position = spawnPosition;
        reward.SetActive(true);
    }

}
