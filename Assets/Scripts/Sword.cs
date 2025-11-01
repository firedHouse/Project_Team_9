using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider swordCollider;
    private Player _player;
    private bool _isQReady = true;
    private bool _isWReady = true;      //사운드딜레이

    public bool attackTime = true; //공격딜레이
    public bool skillTime = true;   //스킬딜레이

    // private float _soundCooldown = 1.5f; 사운드 쿨타임 기존 설정값


    private void Awake()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false;
        GameObject playerObj = GameObject.FindWithTag("Player");
        _player = playerObj.GetComponent<Player>();
    }

    private void Update()
    {
        if (attackTime && _isQReady && Input.GetKeyUp(KeyCode.Q))
        {
            _isQReady = false;
            attackTime = false;
            StartCoroutine(AttackCollider());
            StartCoroutine(CooldownQ());
        }
        if (skillTime && _isWReady && Input.GetKeyDown(KeyCode.W))
        {
            _isWReady = false;
            skillTime = false;
            StartCoroutine(ActivateCollider());
            StartCoroutine(CooldownW());
        }

    }
    private IEnumerator CooldownQ()
    {
        SoundManager.Instance.PlaySFX("knightQ");
        yield return new WaitForSeconds(1.5f);
        _isQReady = true;
    }
    private IEnumerator CooldownW()
    {
        SoundManager.Instance.PlaySFX("Whirlwind");
        yield return new WaitForSeconds(4.0f);
        _isWReady = true;

    }

    private IEnumerator AttackCollider()
    {
        
        yield return new WaitForSeconds(0.5f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        swordCollider.enabled = false;
        
        attackTime = true;

    }

    private IEnumerator ActivateCollider()
    {

        yield return new WaitForSeconds(0.3f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        swordCollider.enabled = false;
        yield return new WaitForSeconds(2.7f);
        skillTime = true;

    }

    private void OnTriggerEnter(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Enemy enemyUnit = enemy.gameObject.GetComponent<Enemy>();

            enemyUnit.TakeDamage(_player.damage);
            Debug.Log($"적에게 {_player.damage}만큼의 데미지 입힘. 현재 적 체력: {enemyUnit.currentHp}");
        }
    }
}
