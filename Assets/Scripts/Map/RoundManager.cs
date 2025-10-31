using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// 라운드 관리 매니저
// 역활 : 라운드 상태 관리, 시간 관리, 적 관리, 다음 방 이동 처리

public class RoundManager : Singleton<RoundManager>
{
    public enum RoundState
    {
        Ready,
        Playing,
        Cleared,
        Failed,
        End
    }

    private RoundState _currentState = RoundState.Ready;
    public RoundState CurrentState => _currentState;

    public event Action<RoundState> OnRoundStateChanged;

    [SerializeField] private float roundTime = 120f;
    private float _timer = 0f;
    public List<Enemy> enemies = new List<Enemy>();

    private bool _isTransitioning = false;

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

        if (_timer <= 0f && !_isTransitioning)
        {
            EndRound(true);
        }
        else if (enemies.Count == 0 && !_isTransitioning)
        {
            EndRound(true);
        }
    }
    // 라운드 시작
    public void StartRound()
    {
        _currentState = RoundState.Playing;
        _timer = roundTime;
        _isTransitioning = false;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log("라운드 시작");
    }
    // 라운드 종료
    private void EndRound(bool cleared)
    {
        _currentState = cleared ? RoundState.Cleared : RoundState.Failed;
        OnRoundStateChanged?.Invoke(_currentState);

        if (cleared)
        {
            StartCoroutine(NextPortalSequence());
        }
        else
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Result);
        }
    }
    
    private IEnumerator NextPortalSequence()
    {
        _isTransitioning = true;
        Debug.Log("5초 뒤 다음 방으로 이동합니다...");
        yield return new WaitForSeconds(5f);

        PortalManager.Instance.MoveToNextPortal();
    }

    public float GetRemainingTime() => _timer;
}
