using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private Player player;
    void Start()
    {
 
    }

    // Update is called once per frame
    void Update()
    {
        if(player.enabled)
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
