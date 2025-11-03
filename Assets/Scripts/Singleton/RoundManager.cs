using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//기존 라운드 관리 매니저 제거하고 간단히 제작
//벡터값 받고 2라운드까지만 이동하게 하드코딩(여현구)

public class RoundManager : Singleton<RoundManager>
{
    //라운드 시간 상수로 지정.
    [SerializeField] private float RoundTime = 115.0f;
    [SerializeField] private float NextRoundTime = 120.0f;

    //플레이어, 경과시간, 진행여부, 매 프레임마다 이동하지 않게 115초에 한 번만 이동하도록 제한하는 필드
    private Player _player;
    private float _elapsedTime = 0f;
    private bool _isRoundRunning = false;
    private bool _logTriggered = false;

    //다음 라운드 위치 직렬화, 기본값 2라운드 시작점으로 지정
    [SerializeField] private Vector3 _nextRoundPosition = new Vector3(138f, 1.5f, -25f);

    protected override void Awake()
    {
        base.Awake();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            _player = playerObj.GetComponent<Player>();
        }
        //상태변경 구독
        GameManager.Instance.OnStateChanged += HandleGameStateChange;
    }

    //플레이 진입 시 타이머 시작하는 메서드 실행하는 메서드
    private void HandleGameStateChange(GameManager.GameState newState)
    {
        if (newState == GameManager.GameState.Play)
        {
            Debug.Log("플레이씬 진입, 라운드시작'");
            RoundStart();
        }
    }
    //라운드 시작하면서 타이머 시작
    public void RoundStart()
    {
        if (_isRoundRunning)
        {
            return;
        }
        _elapsedTime = 0f;
        _logTriggered = false;
        _isRoundRunning = true;
    }

    //다음 라운드로 플레이어 포지션값 이동
    private void RoundEnd()
    {
        _isRoundRunning = false;
        _player.transform.position = _nextRoundPosition;
    }

    //매 업데이트마다 경과시간 누적, 115초에 로그, 120초에 이동
    private void Update()
    {
        if (!_isRoundRunning)
        {
            return;
        }
        _elapsedTime += Time.deltaTime;

        //115초 이후 로그 트리거가 true로 변환되어 1번만 로그가 출력되도록 설정
        if (!_logTriggered && _elapsedTime >= RoundTime)
        {
            Debug.Log("5초 뒤 이동합니다(UI 구현 바랍니다)");
            _logTriggered = true; 
        }
        //120초 경과시 라운드 끝내고 다음 라운드 시작점으로 포지션값 변경
        if (_elapsedTime >= NextRoundTime)
        {
            RoundEnd();
        }
    }
}
