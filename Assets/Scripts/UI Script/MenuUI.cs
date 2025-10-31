using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

     if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }

    public void OnClinkExit()
    {
        Time.timeScale = 1;
        gameObject.SetActive(false);
        Debug.Log("Exit Button Clicked");
    }
    public void OnClinkBacktoLobby()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("TestLobbyScene");
        Debug.Log("Back to Lobby Button Clicked");
    }
}
