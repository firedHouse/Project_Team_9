using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Text resultText;
    private bool _ResultUItrigger = false;

    private Player player;

    private void Awake()
    {
        gameObject.SetActive(false); // 처음에는 숨기기
        GameManager.Instance.OnStateChanged += OnResultUIActive;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= OnResultUIActive;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.gameObject.GetComponent<Player>();
    }

    private void OnResultUIActive(GameManager.GameState state)
    {
        Debug.Log("유저사망");
        if (state == GameManager.GameState.Result)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnClinkBackToLobby()
    {
        _ResultUItrigger = false;

        // 플레이어 체력 초기화
        if (player != null)
            player.currentHp = player.maxHp;
        GameManager.Instance.ChangeState(GameManager.GameState.Lobby);
        gameObject.SetActive(false);
    }
}
