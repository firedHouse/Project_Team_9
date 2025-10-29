using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] private bool x, y, z;
    [SerializeField] private Transform targetTransform;

    // Update is called once per frame
    void Update()
    {
        if(!targetTransform)
        {
            return;
        }
        targetTransform.position = new Vector3(
            x ? transform.position.x : targetTransform.position.x,
            y ? transform.position.y : targetTransform.position.y,
            z ? transform.position.z : targetTransform.position.z
            );
    }
}
