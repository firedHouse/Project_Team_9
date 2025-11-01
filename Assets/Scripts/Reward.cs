using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private float _expAmount = 20;
    //반납할 때 사용할 키 = 프리펩의 이름
    [Header("Pool Key")]
    [SerializeField] private string _prefabKey = "EnemyPrefabName";

    private void OnTriggerEnter(Collider other)
    {
        // 플레이어 태그와 충돌했는지 확인
        if (other.CompareTag("Player"))
        {
            GainExp();
            SoundManager.Instance.PlaySFX("getReward");
            ObjectPoolManager.Instance.ReturnObject(gameObject, gameObject.name);
        }
    }
    private void GainExp()
    {
        //씬 전체에서 오브젝트 찾기
        PlayerStats playerStats = FindObjectOfType<PlayerStats>();
        if (playerStats != null)
        {
            playerStats.GetExp(_expAmount);
            Debug.Log($"경험치 {_expAmount} 획득");
        }
        else
        {
            Debug.Log("PlayScene에 PlayerStats 컴포넌트 넣으세요");
        }
    }

}
