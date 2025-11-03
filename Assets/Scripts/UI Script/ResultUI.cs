using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Player player;
    private bool _ResultUItrigger = false;

    private void OnEnable()
    {
        GameManager.Instance.OnStateChanged += OnResultUIActive;
    }
    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= OnResultUIActive;
    }

    void Update()
    {
        if (!_ResultUItrigger && player != null && player.currentHp <= 0)
        {
            _ResultUItrigger = true;
            GameManager.Instance.ChangeState(GameManager.GameState.Result);
            gameObject.SetActive(true);
        }
    }

    public void OnClinkReGame()
    {
        _ResultUItrigger = false;
        GameManager.Instance.ChangeState(GameManager.GameState.Play);
        gameObject.SetActive(false);
    }

    public void OnClinkBackToLobby()
    {
        _ResultUItrigger = false;
        GameManager.Instance.ChangeState(GameManager.GameState.Lobby);
        gameObject.SetActive(false);
    }
    
    private void OnResultUIActive(GameManager.GameState state)
    {
        gameObject.SetActive(state == GameManager.GameState.Result);
    }
}
