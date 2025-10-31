using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniMapUI : MonoBehaviour
{
    [SerializeField] private Camera miniMapCamera;
    [SerializeField] private float zoomMin = 1f; // 카메라 줌인 최소 크기
    [SerializeField] private float zoomMax = 20f; // 카메라 줌아웃 최대 크기
    [SerializeField] private float zoomOneStep = 1f; // 줌 1회시 증가/감소하는 수치


    public void ZoomIn()
    {
        miniMapCamera.orthographicSize = Mathf.Max(miniMapCamera.orthographicSize - zoomOneStep, zoomMin);
    }
    public void ZoomOut()
    {
        miniMapCamera.orthographicSize = Mathf.Min(miniMapCamera.orthographicSize + zoomOneStep, zoomMax);
    }
}
