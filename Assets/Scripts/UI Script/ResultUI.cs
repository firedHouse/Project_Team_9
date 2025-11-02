using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Player player;

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
        if(player.IsDead)
        {
            gameObject.SetActive(true);
        }
    }

    public void OnClinkReGame()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Play);
        UnityEngine.SceneManagement.SceneManager.LoadScene("HansolTestScene");
        gameObject.SetActive(false);
    }

    public void OnClinkBackToLobby()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Lobby);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLobbyScene");
        gameObject.SetActive(false);
    }
    
    private void OnResultUIActive(GameManager.GameState state)
    {
        if (state == GameManager.GameState.Result)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
