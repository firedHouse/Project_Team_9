using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private Collider swordCollider;
    private Player player;
    private void Awake()
    {
        swordCollider = GetComponent<Collider>();
        swordCollider.enabled = false;

    }
    private void HitDamage()
    {
        // if (딜레이중이라면? )
        // 리턴


        if (Input.GetKeyUp(KeyCode.Q) || Input.GetKeyDown(KeyCode.W))
        {
            StartCoroutine(ActivateCollider());
        }
    }

    private IEnumerator ActivateCollider()
    {   

        yield return new WaitForSeconds(0.5f);
        swordCollider.enabled = true;
        yield return new WaitForSeconds(0.5f);
        swordCollider.enabled = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {            
            Debug.Log("적에게 대미지입힘");
        }
    }

    private void Update()
    {
        HitDamage();

    }
}
