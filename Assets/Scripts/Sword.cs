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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            SoundManager.Instance.PlaySFX("knightQ");
            StartCoroutine(ActivateCollider());
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            SoundManager.Instance.PlaySFX("Whirlwind");
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
}
