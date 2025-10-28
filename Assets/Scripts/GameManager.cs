using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
//게임 상태와 씬 전환을 담당

public class GameManager : Singleton<GameManager>
{

    //씬 이름(실제 씬과 이름 통일 필요, 순서대로 로비, 인게임, 결과)
    public enum SceneName
    {
        LobbyScene, PlayScene, ResultScene,
        End
    }

    //게임 상태 열거형 
    public enum GameState
    {
        Lobby, CharacterSelect, Play, Pause, Result,
        End
    }

    //게임 상태 변경 옵저버 이벤트
    public event Action<GameState> OnStateChanged;

    private Dictionary<GameState, SceneName> _sceneChange = new Dictionary<GameState, SceneName>()
    {
        { GameState.Lobby, SceneName.LobbyScene },
        { GameState.CharacterSelect, SceneName.LobbyScene },
        { GameState.Play, SceneName.PlayScene },
        { GameState.Result, SceneName.ResultScene },
    };

    //초기 상태는 로비, 읽기전용 프로퍼티화
    private GameState _currentState = GameState.Lobby;
    public GameState currentState => _currentState;

    public void ChangeState(GameState state)
    {
        if (_currentState == state)
        {
            return;
        }
        Debug.Log($"게임 State 변경!! {_currentState} -> {state}");

        //퍼즈용 이전 상태 저장
        GameState previousState = _currentState;
        _currentState = state;
        OnStateChanged?.Invoke(state);

        //퍼즈, 씬 전환 로직.
        switch (state)
        {
            //인게임에서만 가능한 퍼즈 상태
            case GameState.Pause:
                if (previousState == GameState.Play)
                {
                    Time.timeScale = 0f;

                    //여현구: UI 출력 관련 로직 넣으시면 됩니다.
                }
                else
                {
                    _currentState = previousState;
                }
                break;

            //인게임 케이스
            case GameState.Play:
                //퍼즈였으면 타임스케일 원복.
                if (previousState == GameState.Pause)
                {
                    Time.timeScale = 1f;
                }

                //퍼즈 상태 아니였다면 원하는 씬으로 이동.
                else
                {
                    //HandleSceneLoad(state);
                }
                break;

            case GameState.Lobby:
            case GameState.CharacterSelect:
            case GameState.Result:
                HandleSceneLoad(state);
                Time.timeScale = 1f;
                break;
        }
    }

    //씬 로드기능
    private void HandleSceneLoad(GameState state)
    {
        if (_sceneChange.ContainsKey(state))
        {
            SceneName sceneToLoad = _sceneChange[state];

            //현재 씬과 다를 경우에만 로드
            if (sceneToLoad != (SceneName)SceneManager.GetActiveScene().buildIndex)
            {
                Debug.Log($"{sceneToLoad}씬 로딩중");
                SceneManager.LoadScene((int)sceneToLoad);
            }
        }
    }
}

