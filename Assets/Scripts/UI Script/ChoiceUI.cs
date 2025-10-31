using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    public Text JobText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }
    private void LateUpdate()
    {
        ChoiceJob();
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

    public void ChoiceJob() // 캐릭터 직업 선택 -> Debug.Log로 확인
    {
        if(JobText.text == "Knight")
        {
            Debug.Log("Knight Choice");
            // PlayerPrefs.SetString("Job", "Knight");
        }
        else if (JobText.text == "Ranger")
        {
            Debug.Log("Ranger Choice");
            // PlayerPrefs.SetString("Job", "Ranger");
        }
        else if (JobText.text == "Mage")
        {
            Debug.Log("Mage Choice");
            // PlayerPrefs.SetString("Job", "Mage");
        }
    }


}

