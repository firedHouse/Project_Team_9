using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Arrow : MonoBehaviour
{
    private Player _player;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            _player = playerObj.GetComponent<Player>();
            if (_player == null)
            {
                Debug.LogError(" Player 오브젝트에 Player 컴포넌트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다.");
        }
    }
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Enemy enemyUnit = enemy.gameObject.GetComponent<Enemy>();

            enemyUnit.TakeDamage(_player.damage);
            Debug.Log($"적에게 {_player.damage}만큼의 데미지 입힘. 현재 적 체력: {enemyUnit.currentHp}");
            Destroy(gameObject);
        }
    }

}
