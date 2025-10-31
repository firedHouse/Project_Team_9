using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherArrow : MonoBehaviour
{
    [SerializeField] private GameObject _arrow;
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private GameObject _activeSkill;
    public void Spawn()
    {
        GameObject arrow = Instantiate(_arrow, transform.position, transform.rotation);
        SoundManager.Instance.PlaySFX("3arrows");
        StartCoroutine(MoveAndDestroy(arrow));  //코루틴 시작
    }

    public void ActiveSkill()
    {
        GameObject arrow = Instantiate(_activeSkill, transform.position, transform.rotation);

        StartCoroutine(MoveAndDestroy(arrow));  //코루틴 시작
    }

    private IEnumerator MoveAndDestroy(GameObject arrow)
    {
        float lifetime = 5f;
        float elapsed = 0f;

        while (elapsed < lifetime)  //5초가 안지났다면 화살전진
        {
            arrow.transform.Translate(Vector3.forward * _arrowSpeed * Time.deltaTime);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(arrow); //5초가 지났다면 파괴
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Spawn();
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            ActiveSkill();
        }
    }
}
