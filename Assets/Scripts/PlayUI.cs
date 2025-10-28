using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    // [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnMenu();
        }
        ExpUp();
    }

    public void OnMenu()
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);
        Debug.Log("Menu Button Clicked");
    }

    private void ExpUp()
    {

    }
}
