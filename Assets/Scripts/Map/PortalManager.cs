using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalManager : Singleton<PortalManager>
{
    // 포탈 프리팹 또는 포탈 오브젝트
    [SerializeField] private GameObject portal; 
    private bool portalActive = false;

    private void Start()
    {
        HidePortal();
    }

    public void ShowPortal()
    {
        if (portal == null)
        {
            Debug.LogWarning("Portal 오브젝트가 설정되지 않았습니다!");
            return;
        }

        portal.SetActive(true);
        portalActive = true;
        Debug.Log("포탈 활성화됨");
    }

    public void HidePortal()
    {
        if (portal != null)
            portal.SetActive(false);

        portalActive = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!portalActive) return;
        if (!other.CompareTag("Player")) return;

        Debug.Log("플레이어가 포탈 진입");
        HidePortal();

        if (RoundManager.Instance != null)
        {
            RoundManager.Instance.StartRound();
        }
        else
        {
            Debug.LogError("RoundManager 인스턴스가 없습니다!");
        }
    }
}
