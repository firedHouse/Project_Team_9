using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class KnightE : MonoBehaviour
{
    private Player player;
    private Collider magicCollider;
    [SerializeField] private GameObject _magic;
    private bool isDelay = false;
    private float elapsed = 2.5f;

    private void Awake()
    {
        player = GetComponent<Player>();
        magicCollider = GetComponent<Collider>();
        magicCollider.enabled = false;
    }

    private IEnumerator Spawn()    //마법생성
    {
        isDelay = true;
        GameObject magic = Instantiate(_magic, transform.position, transform.rotation);
        Destroy(magic, elapsed);    //5초뒤 삭제
        yield return new WaitForSeconds(2.5f);  //쿨타임
        isDelay = false;
    }

    private IEnumerator CooldownE()
    {
        isDelay = true;
        SoundManager.Instance.PlaySFX("KnightE");
        yield return new WaitForSeconds(2.5f);
        isDelay = false;
    }

    private IEnumerator ActivateCollider()  //E스킬 쿨타임
    {        
        magicCollider.enabled = true;
        yield return new WaitForSeconds(2.5f);
        magicCollider.enabled = false;
        isDelay = true;

    }
    private void OnTriggerStay(Collider enemy)
    {
        if (enemy.CompareTag("Enemy"))
        {
            Enemy enemyUnit = enemy.gameObject.GetComponent<Enemy>();

            enemyUnit.TakeDamage(player.damage);
            Debug.Log($"적에게 {player.damage}만큼의 데미지 입힘. 현재 적 체력: {enemyUnit.currentHp}");
        }
    }

    private void Update()
    {
        if (player.ESkillUnlocked && Input.GetKey(KeyCode.E) && isDelay == false)
        {
            StartCoroutine(Spawn());
            StartCoroutine(CooldownE());
            StartCoroutine(ActivateCollider());
        }
    }
}
