using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider swordCollider;
    private Player _player;
    private float _soundCooldown = 1.5f;
    private bool _isQReady = true;
    private bool _isWReady = true;

    private void Awake()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false;
        GameObject playerObj = GameObject.FindWithTag("Player");
        _player = playerObj.GetComponent<Player>();
    }

    private void Update()
    {
        if (_isQReady && Input.GetKeyUp(KeyCode.Q))
        {
            StartCoroutine(ActivateCollider());
            StartCoroutine(CooldownQ());
            _isQReady = false;
        }
        if (_isWReady && Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ActivateCollider());
            StartCoroutine(CooldownW());
            _isWReady = false;
        }

    }
    private IEnumerator CooldownQ()
    {
        SoundManager.Instance.PlaySFX("knightQ");
        yield return new WaitForSeconds(_soundCooldown);
        _isQReady = true;
    }
    private IEnumerator CooldownW()
    {
        SoundManager.Instance.PlaySFX("Whirlwind");
        yield return new WaitForSeconds(_soundCooldown);
        _isWReady = true;

    }

    private IEnumerator ActivateCollider()
    {   

        yield return new WaitForSeconds(0.5f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        swordCollider.enabled = false;

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
