using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickChoiceExit()
    {  
        gameObject.SetActive(false);
        Debug.Log("Choice Exit Button Clicked");
    }
    public void OnClinkGameStart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("HansolTestScene");
        Debug.Log("Game Start Button Clicked");
    }
}
