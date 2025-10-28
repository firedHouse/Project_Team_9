using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Unit
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateInterpolate;

    private Vector3 GetNormalizedDirection()
    {
        Vector3 inputDirection = Vector3.zero;
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.z = Input.GetAxisRaw("Vertical");

        return inputDirection.normalized;
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
    }
}
