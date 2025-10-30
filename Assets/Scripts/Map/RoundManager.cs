using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 사용법
// PlayScene에 RoundManager 스크립트를 추가
// 적 오브젝트들을 enemies 리스트에 할당
// GameManager에서 라운드 상태에 따라 게임 흐름 제어
// RoundManager.Instance.OnRoundStateChanged += (state) => { /* 상태 변화에 따른 처리 */ };
// 라운드 진행 관리

public class RoundManager : Singleton<RoundManager>
{
    // 라운드 상태
    public enum RoundState
    {
        Ready,    
        Playing,    
        Cleared,   
        Failed,    
        End
    }

    // 현재 상태
    private RoundState _currentState = RoundState.Ready;
    public RoundState CurrentState => _currentState;

    // 상태 변경 이벤트
    public event Action<RoundState> OnRoundStateChanged;

    // 라운드 제한 시간 (초 단위)
    [SerializeField] private float roundTime = 120f;
    private float _timer = 0f;

    // 적 리스트 (Scene에서 할당)
    public List<Enemy> enemies = new List<Enemy>();

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        if (_currentState != RoundState.Playing)
            return;

        // 타이머 감소
        _timer -= Time.deltaTime;

        // 적 남아있는지 체크
        enemies.RemoveAll(e => e == null); // 죽은 적 제거

        // 라운드 종료 조건
        if (_timer <= 0f)
        {
            EndRound(true); // 시간 버텼으므로 성공
        }
        else if (enemies.Count == 0)
        {
            EndRound(false); // 적이 다 죽었으면 실패 혹은 클리어 여부
        }
    }

    // 라운드 시작
    public void StartRound()
    {
        _currentState = RoundState.Playing;
        _timer = roundTime;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log("라운드 시작");
    }

    // 라운드 종료
    private void EndRound(bool cleared)
    {
        _currentState = cleared ? RoundState.Cleared : RoundState.Failed;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log(cleared ? "라운드 클리어" : "라운드 실패");

        // 게임 매니저로 상태 전송
        if (cleared)
            GameManager.Instance.ChangeState(GameManager.GameState.Result); 
    }

    // 남은 시간 반환
    public float GetRemainingTime() => _timer;
}

