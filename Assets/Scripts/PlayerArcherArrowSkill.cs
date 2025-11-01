using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArcherArrowSkill : MonoBehaviour
{
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private GameObject _activeSkill;
    private bool isDelay;
    public void ActiveSkill()
    {
        GameObject arrow = Instantiate(_activeSkill, transform.position, transform.rotation);

        StartCoroutine(MoveAndDestroy(arrow));  //코루틴 시작
        StartCoroutine(timer());
    }

    private IEnumerator timer()
    {
        isDelay = true;
        SoundManager.Instance.PlaySFX("3arrows");
        yield return new WaitForSeconds(1.5f);  //쿨타임
        isDelay = false;
        
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
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) == false && Input.GetKeyUp(KeyCode.W) && isDelay == false)
        {           
            ActiveSkill();
        }
    }
}
