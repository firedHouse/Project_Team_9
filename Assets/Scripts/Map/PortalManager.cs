using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 포탈 매니저 : 플레이어를 포탈로 이동시키고, 포탈을 비활성화

public class PortalManager : Singleton<PortalManager>
{
    [System.Serializable]
    public class StagePortals
    {
        public string stageName;
        public List<Transform> portals = new List<Transform>();
    }

    [Header("스테이지별 포탈 목록")]
    public List<StagePortals> stages = new List<StagePortals>();

    private int currentStageIndex = 0;
    private int currentPortalIndex = 0;

    // 플레이어 이동 및 포탈 제어
    public void MoveToNextPortal()
    {
        if (stages.Count == 0 || currentStageIndex >= stages.Count)
        {
            Debug.Log("모든 스테이지 클리어");
            GameManager.Instance.ChangeState(GameManager.GameState.Result);
            return;
        }

        var stage = stages[currentStageIndex];
        if (stage.portals.Count == 0)
        {
            Debug.LogWarning($"스테이지 {currentStageIndex + 1}에 포탈이 없습니다.");
            return;
        }

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogWarning("플레이어를 찾을 수 없습니다. Player 태그를 확인하세요.");
            return;
        }

        // 이동
        Vector3 targetPos = stage.portals[currentPortalIndex].position;
        Rigidbody rb = player.GetComponent<Rigidbody>();
        if (rb != null) rb.MovePosition(targetPos);
        else player.transform.position = targetPos;

        Debug.Log($"플레이어: 스테이지 {currentStageIndex + 1}, 포탈 {currentPortalIndex + 1}로 이동");

        // 이동한 포탈은 비활성화
        stage.portals[currentPortalIndex].gameObject.SetActive(false);

        // 다음 포탈 준비
        currentPortalIndex++;

        // 현재 스테이지 내 모든 포탈 클리어 시 다음 스테이지로 이동
        if (currentPortalIndex >= stage.portals.Count)
        {
            currentPortalIndex = 0;
            currentStageIndex++;

            if (currentStageIndex >= stages.Count)
            {
                Debug.Log("모든 스테이지 클리어!");
                GameManager.Instance.ChangeState(GameManager.GameState.Result);
                return;
            }

            Debug.Log($"다음 스테이지로 이동: Stage {currentStageIndex + 1}");
        }

        // 다음 방으로 이동했을 경우에만 라운드 시작
        if (currentStageIndex < stages.Count)
        {
            RoundManager.Instance.StartRound();
        }
    }

    // 현재 스테이지 번호 반환
    public int GetCurrentStage() => currentStageIndex + 1;
}