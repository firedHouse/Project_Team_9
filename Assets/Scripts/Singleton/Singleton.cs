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
                    Debug.LogError("인스턴스 없어요");
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
            //이미 인스턴스 있고, 서로 다른 경우 (중복)
            //원본 아니니까(이미 있으니까) 파괴
            if (_instance.gameObject != gameObject)
            {
                Destroy(gameObject);
            }
        }

    }
}
