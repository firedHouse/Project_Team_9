using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 씬에 포탈 오브젝트 순서대로 할당
public class PortalManager : Singleton<PortalManager>
{
    public List<Transform> portals = new List<Transform>();
    private int currentPortalIndex = 0;

    public void MoveToNextPortal()
    {
        if (portals.Count == 0) return;

        // 플레이어 찾기
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.transform.position = portals[currentPortalIndex].position;
            Debug.Log($"플레이어 포탈 {currentPortalIndex + 1}로 이동");

            // 다음 포탈 준비
            currentPortalIndex++;
            if (currentPortalIndex >= portals.Count)
            {
                Debug.Log("마지막 포탈에 도착했습니다!");
                // 마지막 포탈 고정
                currentPortalIndex = portals.Count - 1; 
            }
        }

        // 라운드 재시작
        RoundManager.Instance.StartRound();
    }
}

