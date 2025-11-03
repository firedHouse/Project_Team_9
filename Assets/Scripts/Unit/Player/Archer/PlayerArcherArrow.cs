using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerArcherArrow : MonoBehaviour
{
    private Player _player;
    private float _arrowDamage;
    [SerializeField] private GameObject _arrow;
    [SerializeField] private float _arrowSpeed;
    private bool isDelay;
    
    public void Spawn()
    {
        GameObject arrow = Instantiate(_arrow, transform.position, transform.rotation);
        StartCoroutine(MoveAndDestroy(arrow));  //코루틴 시작
        StartCoroutine(timer());
    }

    private IEnumerator timer()
    {
        isDelay = true;
        SoundManager.Instance.PlaySFX("arrowShot");
        yield return new WaitForSeconds(1.0f);  //쿨타임
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

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isDelay == false)
        {
           Spawn();           
        }
    }
}
