using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider _swordCollider;
    private Player _player;
    private bool _isQReady = true;
    private bool _isWReady = true;
    private bool _isEReady = true;  //사운드딜레이

    private bool _attackTime = true; //공격딜레이
    private bool _wSkillTime = true;
    private bool _eSkillTime = true;  //스킬딜레이

    // private float _soundCooldown = 1.5f; 사운드 쿨타임 기존 설정값


    private void Awake()
    {
        _swordCollider = GetComponent<Collider>();
        _swordCollider.enabled = false;
        GameObject playerObj = GameObject.FindWithTag("Player");
        _player = playerObj.GetComponent<Player>();
    }

    private void Update()
    {
        if (_attackTime && _isQReady && Input.GetKeyUp(KeyCode.Q))
        {
            _isQReady = false;
            _attackTime = false;
            StartCoroutine(AttackCollider());
            StartCoroutine(CooldownQ());
        }
        if (_player.WSkillUnlocked && _wSkillTime && _isWReady && Input.GetKeyDown(KeyCode.W))
        {
            Debug.Log("w스킬 발동");
            _isWReady = false;
            _wSkillTime = false;
            StartCoroutine(ActivateCollider());
            StartCoroutine(CooldownW());
        }
        if (_player.ESkillUnlocked && _isEReady && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E스킬 발동");
            _isEReady = false;
            _eSkillTime = false;
            StartCoroutine(CooldownE());
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
    private IEnumerator CooldownE()
    {
        SoundManager.Instance.PlaySFX("KnightE");
        yield return new WaitForSeconds(5.0f);
        _isEReady = true;

    }

    private IEnumerator AttackCollider()    //기본공격 쿨타임
    {
        
        yield return new WaitForSeconds(0.5f);
        _swordCollider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        _swordCollider.enabled = false;

        _attackTime = true;

    }

    private IEnumerator ActivateCollider()  //W스킬 쿨타임
    {

        yield return new WaitForSeconds(0.3f);
        _swordCollider.enabled = true;
        yield return new WaitForSeconds(1.0f);
        _swordCollider.enabled = false;
        yield return new WaitForSeconds(2.7f);
        _wSkillTime = true;

    }

    //private IEnumerator ActivateCollider()  //E스킬 쿨타임
    //{
    //
    //    yield return new WaitForSeconds(0.3f);
    //    _swordCollider.enabled = true;
    //    yield return new WaitForSeconds(1.0f);
    //    _swordCollider.enabled = false;
    //    yield return new WaitForSeconds(2.7f);
    //    _eSkillTime = true;
    //
    //}

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
