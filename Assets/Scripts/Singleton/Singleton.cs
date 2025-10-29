using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 각종 매니저 상속용 제네릭 기반 클래스
// MonoBehaviour 상속받는 클래스로 제한
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    //외부 호출용 프로퍼티, 해당 타입의 싱글톤이 없으면 찾아보고, 없을 시 새로 생성 후 설정
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    //설계도 없으므로 new
                    GameObject singletoneObj = new GameObject();
                    _instance = singletoneObj.AddComponent<T>();
                    singletoneObj.name = typeof(T).ToString();
                }
            }

            return _instance;
        }
    }
    //중복체크 및 연결 기능 구현
    protected virtual void Awake()
    {
        if (_instance == null)
        {
            _instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }

    }
}
