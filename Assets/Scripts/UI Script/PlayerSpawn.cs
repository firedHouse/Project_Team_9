using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
    public Transform spawnPoint;
    public GameObject knightPrefab;
    public GameObject archerPrefab;

    private void Start()
    {
        SpawnPlayer();
    }

    private void SpawnPlayer()
    {
        string selectedJob = PlayerPrefs.GetString("Job", "Knight"); // 기본값은 Knight
        GameObject playerPrefab = null; // 초기화

        // 선택된 직업에 따라 프리팹 가져오기
        switch (selectedJob)
        {
            case "Knight":
                playerPrefab = knightPrefab;
                break;
            case "Archer":
                playerPrefab = archerPrefab;
                break;
            default:
                Debug.LogError("Unknown job selected: " + selectedJob);
                playerPrefab = knightPrefab; // 기본값으로 설정
                return;
        }

        // 프리팹과 스폰 포인트가 유효한지 확인 후 생성.
        if (playerPrefab != null && spawnPoint != null)
        {
            Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

}
