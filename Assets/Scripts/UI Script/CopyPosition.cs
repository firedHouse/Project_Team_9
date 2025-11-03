using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyPosition : MonoBehaviour
{
    [SerializeField] private bool x, y, z;
    private Transform target;

    void Start()
    {
        StartCoroutine(FindTarget());
    }

    private IEnumerator FindTarget()
    {
        while (Player.Instance == null)
        {
            yield return new WaitForSeconds(0.2f);
        }

        target = Player.Instance.transform;
    }

    void Update()
    {
        if(target == null)
        {
            return;
        }
        transform.position = new Vector3(
            x ? target.position.x : transform.position.x,
            y ? target.position.y : transform.position.y,
            z ? target.position.z : transform.position.z
            );
    }
}
