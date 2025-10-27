using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayUI : MonoBehaviour
{
    [SerializeField] private GameObject menuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClinkMenu()
    {
        menuUI.SetActive(true);
        Debug.Log("Menu Button Clicked");
    }

}
