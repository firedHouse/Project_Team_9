using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 라운드 관리 매니저
// 역활 : 라운드 상태 관리, 시간 관리, 적 관리, 다음 방 이동 처리

public class RoundManager : Singleton<RoundManager>
{
    public enum RoundState { Ready, Playing, Cleared, Failed, End }

    private RoundState _currentState = RoundState.Ready;
    public RoundState CurrentState => _currentState;

    public event Action<RoundState> OnRoundStateChanged;
    // UI 메시지용
    public event Action<string> OnMessage; 

    [SerializeField] private float roundTime = 120f;
    private float _timer = 0f;

    public List<Enemy> enemies = new List<Enemy>();
    private bool _nextRoomWarningShown = false;

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        if (_currentState != RoundState.Playing)
            return;

        _timer -= Time.deltaTime;
        enemies.RemoveAll(e => e == null);

        // 1:55 혹은 몬스터 전멸 시 메시지
        if (!_nextRoomWarningShown && (_timer <= roundTime - 115f || enemies.Count == 0))
        {
            _nextRoomWarningShown = true;
            OnMessage?.Invoke("5초 뒤 다음 방으로 이동합니다!");
            StartCoroutine(NotifyAndWait(5f));
        }

        if (_timer <= 0f)
        {
            EndRound(true);
        }
    }

    public void StartRound()
    {
        _currentState = RoundState.Playing;
        _timer = roundTime;
        _nextRoomWarningShown = false;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log("라운드 시작");
    }

    private void EndRound(bool cleared)
    {
        _currentState = cleared ? RoundState.Cleared : RoundState.Failed;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log(cleared ? "라운드 클리어" : "라운드 실패");
    }

    private IEnumerator NotifyAndWait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        PortalManager.Instance.MoveToNextPortal();
    }
}
