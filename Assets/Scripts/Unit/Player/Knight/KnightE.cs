using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightE : MonoBehaviour
{
    private Player player;
    [SerializeField] private GameObject _magic;
    private bool isDelay = false;
    private float elapsed = 5f;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private IEnumerator Spawn()    //마법생성
    {
        isDelay = true;
        GameObject magic = Instantiate(_magic, transform.position, transform.rotation);
        Destroy(magic, elapsed);    //5초뒤 삭제
        yield return new WaitForSeconds(5.0f);  //쿨타임
        isDelay = false;
    }

    private void Update()
    {
        if (player.ESkillUnlocked && Input.GetKey(KeyCode.E) && isDelay == false)
        {
            StartCoroutine(Spawn());
        }
    }
}
