using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyScanner : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private LayerMask _layerMask;
        private Transform _randomTarget;
        //private Transform _nearestTarget;
        private Collider[] _colliders;

    // 충돌 범위 내에서 무작위 대상 지정
    public Transform GetRandom(float range)
    {
        Transform result = null;
        int rnd = 0;
        // 플레이어 위치에서 range만큼 스캔 범위 지정, _layerMask에 해당하는 대상만 스캔
        _colliders = Physics.OverlapSphere(transform.position, range, _layerMask);
        // 범위 내 적이 있는지 확인
        if (_colliders.Length > 0)
        {
            // 적 배열 중 랜덤 대상 지정하여 transform 반환
            rnd = Random.Range(0, _colliders.Length);
            Debug.Log($"[EnemyScanner] 공격 대상 : {rnd}");
            result = _colliders[rnd].transform;
        }

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
