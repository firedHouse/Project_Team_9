using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject powerUpUI;
    [SerializeField] private GameObject OptionUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClinkStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HansolTestScene");
        Debug.Log("Start Button Clicked");
    }
    public void OnClinkPowerUp()
    {
        powerUpUI.SetActive(true);
        Debug.Log("PowerUp Button Clicked");
    }
    public void OnClinkOptions()
    {
        OptionUI.SetActive(true);
        Debug.Log("Options Button Clicked");
    }
    public void OnClinkQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Debug.Log("Quit Button Clicked");
    }

}
