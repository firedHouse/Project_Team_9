using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Text resultText;
    private bool _ResultUItrigger = false;
    private Player player;

    private void OnEnable()
    {
        GameManager.Instance.OnStateChanged += OnResultUIActive;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= OnResultUIActive;
    }

    private void Start()
    {
        StartCoroutine(WaitForPlayer());
        gameObject.SetActive(false); // 처음에는 숨기기
    }

    private IEnumerator WaitForPlayer()
    {
        // Player.Instance가 생성될 때까지 대기
        while (Player.Instance == null)
        {
            yield return null;
        }

        player = Player.Instance;
    }

    void Update()
    {
        if (_ResultUItrigger || player == null) return;

        // Player HP가 0 이하이면 Result UI 발동
        if (player.currentHp <= 0)
        {
            _ResultUItrigger = true;
            GameManager.Instance.ChangeState(GameManager.GameState.Result);
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
    
    private void OnResultUIActive(GameManager.GameState state)
    {
        gameObject.SetActive(state == GameManager.GameState.Result);
    }
}
