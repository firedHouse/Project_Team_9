using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //플레이어 감지 사거리
    [Header("Detection Settings")]
    [SerializeField] private float _detectionRange = 50f;
    private bool _isSpawningActive = false;


    //스트링 배열로 변경하고 헤더를 좀 더 직관적으로 바꿨습니다
    [Header("Spawn Targets Name")]
    [SerializeField] private string[] _enemyPrefabNames;


    //순서대로 스폰범위, 랜덤포지션값에서 반경 m만큼 설치 가능 위치 검색, 스폰 주기
    [Header("Spawn Settings")]
    [SerializeField] private float _spawnRadius = 50f;
    [SerializeField] private float _navMeshSearchRange = 5f; // NavMesh 검색 반경
    [SerializeField] private float _spawnInterval = 2.0f;

    private Transform _playerTransform;
    private bool _isGamePlaying = false;

    private void Start()
    {

        //플레이어 오브젝트 태그를 추적
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
            Debug.Log("플레이어 태그 찾았다!");
        }
        //게임 상태 변경 구독!
        GameManager.Instance.OnStateChanged += OnGameStateChanged;

    }

    //라운드마다 스포너가 작동하도록 유저와의 거리를 탐지해 스폰하도록 수정
    private void Update()
    {
        if (!_isGamePlaying || _playerTransform == null)
        {
            // 게임이 Play 상태가 아닐 때 스폰이 켜져 있으면 스폰 끝내기
            if (_isSpawningActive)
            {
                CancelInvoke(nameof(SpawnEnemy));
                _isSpawningActive = false;
                Debug.Log("플레이상태아님");
            }
            return;
        }

        float distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);

        //감지 범위 안에 들어왔는지 체크
        if (distanceToPlayer <= _detectionRange)
        {
            // 범위 안에 있고, 아직 스폰이 활성화되지 않았다면 시작
            if (!_isSpawningActive)
            {
                InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnInterval);
                _isSpawningActive = true;
                Debug.Log($"{gameObject}범위 안에 들어와서 스폰 시작");

            }
        }
        else
        {
            // 범위 밖에 있고, 스폰이 활성화되어 있다면 중지
            if (_isSpawningActive)
            {
                CancelInvoke(nameof(SpawnEnemy));
                _isSpawningActive = false;
                Debug.Log("범위 밖으로 나감");

            }
        }



    }
    private void OnDestroy()
    {
        if (GameManager.Instance != null)
        {
            //구독 해제
            GameManager.Instance.OnStateChanged -= OnGameStateChanged;
        }
        CancelInvoke(nameof(SpawnEnemy));
    }

    //Play 여부 판단 메서드
    private void OnGameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Play)
        {
            _isGamePlaying = true;
        }
        else
        {
            _isGamePlaying = false;
        }
    }
    //에너미 스폰 메서드
    private void SpawnEnemy()
    {
        if (_playerTransform == null || _enemyPrefabNames.Length == 0)
        {
            return;
        }

        Vector3 spawnPosition = GetSpawnPosition();

        if (spawnPosition == Vector3.zero)
        {
            Debug.Log("스폰할 곳 없음, Bake 확인");
            return;
        }


        //오브젝트 풀링 도입 완료, 프리펩 이름으로 랜덤 돌려서 하나 생성, null 반환 안전장치 추가
        string randomEnemyPrefab = _enemyPrefabNames[UnityEngine.Random.Range(0, _enemyPrefabNames.Length)];
        GameObject newEnemy = ObjectPoolManager.Instance.GetObject(randomEnemyPrefab);

        if (newEnemy != null)
        {
            newEnemy.transform.position = spawnPosition;
            newEnemy.SetActive(true);
            Enemy enemyOnSpanwed = newEnemy.GetComponent<Enemy>();
            if (enemyOnSpanwed != null)
            {
                enemyOnSpanwed.OnSpawned();
            }
        }
    }

    //주변 오브젝트 체크용으로 네비매쉬 사용
    private Vector3 GetSpawnPosition()
    {
        NavMeshHit hit;

        for (int i = 0; i < 15; i++)
        {
            //반지름이 radius인 구에서 무작위 점을 지정하기
            Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * _spawnRadius;
            randomDirection += transform.position;

            //네비매쉬 Bake 된 곳 찾아서 리턴
            if (NavMesh.SamplePosition(randomDirection, out hit, _navMeshSearchRange, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }
}
