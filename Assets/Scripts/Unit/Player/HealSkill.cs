using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class HealSkill : MonoBehaviour
{
    
    [SerializeField] private GameObject _magic;
    private PlayerStats playerStats;

    private bool isDelay = false;
    private float elapsed = 5f;
    private bool eSkillUnlocked = false;

    private void Start()
    {
        //플레이어스텟정보 가져오기
        
        playerStats = FindObjectOfType<PlayerStats>();        

        if (playerStats != null)
        {
            playerStats.OnLevelChanged += CheckLevel;
        }
    }

    private void CheckLevel(int currentLevel, int maxLevel)
    {
        
        if (currentLevel >= 6 && eSkillUnlocked == false)
        {
            eSkillUnlocked = true;
            Debug.Log("E 스킬 해금");
        }
    }

    private IEnumerator Spawn()    //마법생성
    {
        isDelay = true;
        GameObject magic = Instantiate(_magic, transform.position, transform.rotation);        
        Destroy(magic, elapsed);        //elapsed초뒤 삭제
        yield return new WaitForSeconds(5.0f);  //쿨타임
        isDelay = false;
    }
    private IEnumerator timer() //마법사운드
    {
        isDelay = true;
        SoundManager.Instance.PlaySFX("ArcherE");
        yield return new WaitForSeconds(5.0f);  //쿨타임
        isDelay = false;

    }

    


    private void Update()
    {
        if (eSkillUnlocked && Input.GetKey(KeyCode.E) && isDelay == false)
        {
            StartCoroutine(Spawn());
            StartCoroutine(timer());
        }
    }
}
