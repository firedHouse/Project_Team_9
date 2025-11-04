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

    //데미지 텍스트 프리팹
    [Header("Damage Text Prefab")]
    [SerializeField] private GameObject dmgTextPrefab;

    [Header("Attack Settings")]
    [SerializeField] private float _attackCooldown = 1.0f;
    private float _lastAttackTime = 0f;



    private float _lastUpdateTime = 0f;
    private float _DestinationInterval = 3f;
    private Transform _playerTransform;
    private NavMeshAgent navMeshAgent;

    protected override void Awake()
    {
        base.Awake();

    }
    private void OnEnable()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        if (navMeshAgent != null)
        {
            navMeshAgent.enabled = true;
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
        StopAllCoroutines();
        if (navMeshAgent == null) navMeshAgent = GetComponent<NavMeshAgent>();

        currentHp = maxHp;

        // 항상 에이전트 비활성 상태에서 시작 (프리팹에서도 비활성화 권장)
        navMeshAgent.enabled = false;

        // NavMesh 근처 좌표로 보정 후 에이전트 활성화
        Vector3 spawnPos = transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPos, out hit, 5f, NavMesh.AllAreas))
        {
            transform.position = hit.position;
            StartCoroutine(AgentSetDistance()); // 여기서 AgentSetDistance가 Warp+enable을 수행
        }
        else
        {
            Debug.LogError($"{name} : NavMesh 위치 찾기 실패, 풀로 반납");
            ObjectPoolManager.Instance.ReturnObject(gameObject, _prefabKey);
        }
    }

    IEnumerator AgentSetDistance()
    {
        yield return null;
        navMeshAgent.enabled = true; // 이제 활성화
        yield return null;
        if (navMeshAgent.Warp(transform.position))
        {
            navMeshAgent.speed = moveSpeed;
            navMeshAgent.isStopped = false;

            if (_playerTransform != null)
            {
                navMeshAgent.SetDestination(_playerTransform.position);
                _lastUpdateTime = Time.time;
            }
        }
        else
        {
            Debug.LogError("NavMeshAgent.Warp() 실패: 스폰 위치가 NavMesh와 너무 멉니다.", gameObject);
            ObjectPoolManager.Instance.ReturnObject(gameObject, _prefabKey);
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
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (Time.time >= _lastAttackTime + _attackCooldown)
            {
                Player playerUnit = other.gameObject.GetComponent<Player>();
                playerUnit.TakeDamage(damage);
                Debug.Log("플레이어와 충돌하여 데미지");
                _lastAttackTime = Time.time;
            }
        }
    }
    public override void TakeDamage(float damage)
    {
        currentHp -= damage;

        ShowDamageText(damage);

        Debug.Log($"Enemy가 {damage} 데미지를 받았습니다. 남은 체력: {currentHp}");

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }

    protected override void Die()
    {
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

    private void ShowDamageText(float damage)
    {

        if (dmgTextPrefab == null)
        {
            Debug.Log("오브젝트를 가져올 수 없습니다.");
        }

        Vector3 dmgTxtPos = transform.position + Vector3.up * 2f;
        GameObject dmgTxtObj = Instantiate(dmgTextPrefab, dmgTxtPos, Quaternion.identity);

        DmgTxt dmgTextComponent = dmgTextPrefab.GetComponent<DmgTxt>();

        if (dmgTextComponent != null)
        {
            dmgTextComponent.DisplayDamage(damage);
        }
    }
}
