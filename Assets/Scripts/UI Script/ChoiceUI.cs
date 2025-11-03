using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ChoiceUI : MonoBehaviour
{
    public Text JobText;
    public Dropdown JobDropdown;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //Dropdown 값이 바뀔 때마다 ChoiceJob 함수 호출
        JobDropdown.onValueChanged.AddListener(ChoiceJob);
        ChoiceJob(JobDropdown.value);
    }
    // Choice UI 끄기
    public void OnClickChoiceExit()
    {  
        gameObject.SetActive(false);
        Debug.Log("Choice Exit Button Clicked");
    }
    // 게임 시작 버튼 클릭 -> 게임 상태 Play로 변경
    public void OnClinkGameStart()
    {
        GameManager.Instance.ChangeState(GameManager.GameState.Play);
        Debug.Log("Game Start Button Clicked");
    }

    // 캐릭터 직업 선택 함수
    public void ChoiceJob(int index)
    {   //Dropdown에서 선택된 값 가져오기
        string selectedJob = JobDropdown.options[index].text;
        JobText.text = selectedJob;
        // 선택된 직업에 따라 PlayerPrefs에 저장
        if (selectedJob == "Knight")
        {
            Debug.Log("Knight Choice");
            PlayerPrefs.SetString("Job", "Knight");
        }
        else if (selectedJob == "Archer")
        {
            Debug.Log("Archer Choice");
            PlayerPrefs.SetString("Job", "Archer");
        }
        // 변경된 값 저장
        PlayerPrefs.Save();
    }

}