using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    //스트링 배열로 변경하고 헤더를 좀 더 직관적으로 바꿨습니다
    [Header("Spawn Targets Name")]
    [SerializeField] private string[] _enemyPrefabNames;

    //일단 맵 전체를 바운더리로 설정하여 스포너 하나만 사용해볼 예정
    [Header("Spawner Boundary")]
    [SerializeField] private Vector2 _mapMinXZ = new Vector2(-50f, -50f);
    [SerializeField] private Vector2 _mapMaxXZ = new Vector2(50f, 50f);
    [SerializeField] private float _navMeshSearchRange = 10f; // NavMesh 검색 반경

    //스폰 주기만 있는데 뭔가 더 있는 게 좋을 듯
    [Header("Spawn Settings")]
    [SerializeField] private float _spawnInterval = 2.0f;

    private Transform _playerTransform;

    private void Start()
    {
        //플레이어 오브젝트 태그를 추적
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            _playerTransform = playerObj.transform;
        }

        //게임 상태 변경 구독!
        GameManager.Instance.OnStateChanged += OnGameStateChanged;
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

    //Play 여부에 따라 스폰 메서드 On, off 판단하는 메서드
    private void OnGameStateChanged(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Play)
        {
            //Play 상태가 되면 스폰 메서드를 _spawnInterval마다 호출
            InvokeRepeating(nameof(SpawnEnemy), 0f, _spawnInterval);
            Debug.Log("스폰 메서드 호출할거임");
        }
        else
        {
            //Play 아니면 반복 호출 끄기
            CancelInvoke(nameof(SpawnEnemy));
            Debug.Log("Enemy Spawner: InvokeRepeating 중지.");
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
        Vector3 finalPosition = Vector3.zero;
        NavMeshHit hit;

        //조건 체크항목인데 시간 되면 do while로 리팩토링 할 듯
        for (int i = 0; i < 15; i++)
        {
            //경계 내 무작위 위치값 받아오고
            float randomX = UnityEngine.Random.Range(_mapMinXZ.x, _mapMaxXZ.x);
            float randomZ = UnityEngine.Random.Range(_mapMinXZ.y, _mapMaxXZ.y);
            Vector3 randomPosition = new Vector3(randomX, 0f, randomZ);

            if (NavMesh.SamplePosition(randomPosition, out hit, _navMeshSearchRange, NavMesh.AllAreas))
            {
                finalPosition = hit.position;
                return finalPosition;
            }
        }
        return Vector3.zero;
    }
}
