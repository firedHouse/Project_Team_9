using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Player player;

    void Start()
    {
        if (player == null)
        {
            player = GetComponent<Player>();
            if (player == null)
            {
                Debug.LogError("참조된 Player가 존재하지 않습니다.");
                return;
            }
        }
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
        UnityEngine.SceneManagement.SceneManager.LoadScene("HansolTestScene");
        gameObject.SetActive(false);
    }

    public void OnClinkBackToLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLobbyScene");
        gameObject.SetActive(false);
    }
}
