using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float arrowDamage;
    private Player _player;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindWithTag("Player");
        _player = playerObj.GetComponent<Player>();
    }
    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Enemy enemyUnit = enemy.gameObject.GetComponent<Enemy>();

            enemyUnit.TakeDamage(arrowDamage);
            Debug.Log($"적에게 {arrowDamage}만큼의 데미지 입힘. 현재 적 체력: {enemyUnit.currentHp}");
            Destroy(gameObject);
        }
    }

}
