using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundManager : Singleton<RoundManager>
{
    public enum RoundState
    {
        Ready,
        Playing,
        Cleared,
        End
    }

    private RoundState _currentState = RoundState.Ready;
    public RoundState CurrentState => _currentState;

    public event System.Action<RoundState> OnRoundStateChanged;

    [SerializeField] private float roundTime = 120f;
    private float _timer;

    private List<Enemy> enemies = new List<Enemy>();
    private bool roundEnded = false;

    private void Start()
    {
        StartRound();
    }

    private void Update()
    {
        if (_currentState != RoundState.Playing) return;

        _timer -= Time.deltaTime;

        // Scene에 남아있는 모든 Enemy 자동 추적
        enemies.Clear();
        enemies.AddRange(FindObjectsOfType<Enemy>());

        if (!roundEnded && enemies.Count == 0)
        {
            roundEnded = true;
            StartCoroutine(HandleRoundClear());
        }

        if (_timer <= 0f && !roundEnded)
        {
            roundEnded = true;
            StartCoroutine(HandleRoundClear());
        }
    }

    public void StartRound()
    {
        _currentState = RoundState.Playing;
        _timer = roundTime;
        roundEnded = false;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log("라운드 시작");

        // 포탈 숨기기
        if (PortalManager.Instance != null)
            PortalManager.Instance.HidePortal();
    }

    private IEnumerator HandleRoundClear()
    {
        _currentState = RoundState.Cleared;
        OnRoundStateChanged?.Invoke(_currentState);
        Debug.Log("라운드 클리어");

        yield return new WaitForSeconds(5f); 

        if (PortalManager.Instance != null)
            PortalManager.Instance.ShowPortal(); 
    }
}
