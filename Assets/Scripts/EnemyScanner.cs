using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] public Transform _nearestTarget;
    [SerializeField] public Transform _randomTarget;
    private Collider[] _colliders;

    // 충돌 범위 내에서 무작위 대상 지정
    public Transform GetRandom(float range)
    {
        _colliders = Physics.OverlapSphere(transform.position, range, _layerMask);
        Transform result = null;
        string tag = "Enemy";
        int rnd = Random.Range(0, _colliders.Length);
        Debug.Log($"[EnemyScanner] 공격 대상 : {rnd}");

        result = _colliders[rnd].transform;
        return result;
    }

    // 충돌 범위 내에서 가장 가까운 대상 지정
    // 3단계 투사체 갯수 증가 시 사용할까봐 삭제안했습니다
    public Transform GetNearest()
    {
        Transform result = null;
        float diff = 1000f;
        string tag = "Enemy";
        
        //foreach(RaycastHit hit in _targets)
        foreach(var hit in _colliders)
        {
            // 타겟 태그로 필터링
            if (hit.gameObject.tag == tag)
            {
                Vector3 playerPosition = transform.position;
                Vector3 targetPosition = hit.transform.position;
                float curDiff = Vector3.Distance(playerPosition, targetPosition);

                if(curDiff < diff)
                {
                    diff = curDiff;
                    result = hit.transform;
                }
                //Debug.Log($"{diff}");
            }
        }
        return result;
    }
}
