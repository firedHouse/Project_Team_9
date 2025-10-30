using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    [SerializeField] private Slider expBar;
    [SerializeField] private Slider hpBar;


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

        // expBar.value = Player.Instance.Exp / Player.Instance.MaxExp;

    }

    public void OnMenu()
    {
        Time.timeScale = 0;
        menuUI.SetActive(true);
        Debug.Log("Menu Button Clicked");
    }

    public void LevelUp()
    {
        
        
    }


}
