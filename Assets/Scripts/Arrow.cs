using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{    
    private void OnTriggerEnter(Collider other)
    {        
        if (other.CompareTag("Enemy"))  // Enemy 태그가진 오브젝트을 만나면 파괴
        {
            Debug.Log("적에게 대미지입힘");
            Destroy(gameObject);
        }
    }
}
