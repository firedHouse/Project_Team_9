using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnMenu();
        }

    }

    public void OnMenu()
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);
        Debug.Log("Menu Button Clicked");
    }

}
