using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private Transform target; // 추적 대상 (플레이어 게임 오브젝트)
    [SerializeField] private float smoothSpeed = 0.05f; // 카메라 이동의 부드러움 정도

    float dist = 5f; // 카메라와 추적 대상 간의 거리
    float height = 5f; // 카메라의 높이

    void Start()
    {

    }

    void LateUpdate()
    {
        // 카메라의 위치를 추적대상의 dist 변수만큼 뒤로 배치하고
        // height 변수만큼 위로 올림
        this.transform.position = Vector3.Lerp(this.transform.position, target.position - (target.forward * dist) + (Vector3.up * height), Time.deltaTime * smoothSpeed);
        this.transform.LookAt(target); // 카메라가 플레이어 게임 오브젝트를 바라보게 설정
    }
}
