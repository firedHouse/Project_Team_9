using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Unit
{
    private bool isDead = false;
    private float rotateInterpolate = 10;    

    public bool isDelay;    

    [SerializeField] private Animator animator;       

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private Vector3 GetNormalizedDirection()
    {
        Vector3 inputDirection = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputDirection.z += 1;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputDirection.z -= 1;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputDirection.x += 1;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputDirection.x -= 1;
        }
        return inputDirection.normalized;
    }

    private void ActiveSkill()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            if (isDelay == false)
            {
                isDelay = true;                
                StartCoroutine(Attack());

            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (isDelay == false)
            {
                isDelay = true;
                StartCoroutine(ActiveSkillW());

            }
        }
        
        if (Input.GetKeyUp(KeyCode.E))
        {
            Debug.Log("E 속성스킬");
        }

        //if (Input.GetKey(KeyCode.R))
        //{
        //    R스킬 
        //}

        

        //if (Input.GetKey(KeyCode.Space) != dash)
        //{
        //    아직 구현못함
        //}
    }

    IEnumerator Attack()
    {
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);        
        isDelay = false;
    }
    IEnumerator ActiveSkillW()
    {
        animator.SetTrigger("Skill W");
        yield return new WaitForSeconds(1.5f);
        isDelay = false;
    }
    private void SetPosition()
    {
        Vector3 direction = GetNormalizedDirection();

        animator.SetBool("Run", direction != Vector3.zero);

        if (direction == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), rotateInterpolate * Time.deltaTime);

        transform.position += moveSpeed * Time.deltaTime * direction;
    }

    public override void TakeDamage(float damage)
    {
        if (isDead)
        {
            return;
        }

        currentHp -= damage;
        Debug.Log($"플레이어가 {damage} 데미지를 받았습니다. 남은 체력: {currentHp}");

        if (currentHp <= 0)
        {
            currentHp = 0;
            Die();
        }
    }


    protected override void Die()
    {
        Debug.Log($"You died");

        StartCoroutine(GameOverSequence(5f));
    }

    private IEnumerator GameOverSequence(float delay)
    {
        Debug.Log("씬전환됨");
        enabled = false;
        yield return new WaitForSeconds(delay);
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeState(GameManager.GameState.Result);
        }
        //Destroy(gameObject) <= 계속 씬 전환 일어날 시 사용
    }

    private void Update()
    {
        SetPosition();
        ActiveSkill();
    }
}
