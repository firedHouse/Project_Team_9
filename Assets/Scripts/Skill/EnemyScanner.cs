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
}
