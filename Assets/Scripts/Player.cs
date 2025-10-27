using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateInterpolate;

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
        if (Input.GetKey(KeyCode.Q))
        {
            Debug.Log("Q입력");
        }

        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W입력");
        }

        if (Input.GetKey(KeyCode.E))
        {
            Debug.Log("E입력");
        }

        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("R입력");
        }
    }
    private void SetPosition()
    {
        Vector3 direction = GetNormalizedDirection();
        if (direction == Vector3.zero)
        {
            return;
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), _rotateInterpolate * Time.deltaTime);

        transform.position += _moveSpeed * Time.deltaTime * direction;
    }
    
    
    //private void MovePlayer()
    //{
    //
    //}


    //public override void Die()
    //{
    //    Debug.Log($"게임종료");
    //}

    private void Update()
    {
        SetPosition();
        ActiveSkill();
    }
}
