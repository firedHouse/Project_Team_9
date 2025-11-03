using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject powerUpUI;
    [SerializeField] private GameObject optionUI;
    [SerializeField] private GameObject choiceUI;

    void Start()
    {
        if (powerUpUI != null) powerUpUI.SetActive(false);
        if (optionUI != null) optionUI.SetActive(false);
        if (choiceUI != null) choiceUI.SetActive(false);
    }

    void Update()
    {
        
    }

    public void OnClinkStart()
    {
        choiceUI.SetActive(true);
        Debug.Log("Start Button Clicked");
    }
    public void OnClinkPowerUp()
    {
        powerUpUI.SetActive(true);
        Debug.Log("PowerUp Button Clicked");
    }
    public void OnClinkOptions()
    {
        optionUI.SetActive(true);
        Debug.Log("Options Button Clicked");
    }
    public void OnClinkQuit()
    {
#if UNITY_EDITOR // 신기해서 적용해보고 싶음. Unity Editor에서 플레이 모드를 종료하는 코드
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 빌드된 애플리케이션에서 실행 시 애플리케이션 종료 라고 하는데 맞는지 모르겠음. 실험 해봐야할듯.
#endif
        Debug.Log("Quit Button Clicked");

    }

}
