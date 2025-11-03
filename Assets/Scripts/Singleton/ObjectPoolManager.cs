using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//오브젝트 풀링을 담당하는 매니저
//여기에 프리팹을 직렬화하여 미리 생성해두는 식으로 제작할 예정.
//구현해야 할 메서드
//GetObject, ReturnObject 
//풀에 오브젝트 생성해두는 메서드를 Awake? start? 에 제작 InitializePools


[System.Serializable]
public class PoolData
{
    public GameObject prefab;
    public int initialSize;
}

public class ObjectPoolManager : Singleton<ObjectPoolManager>
{
    private Dictionary<string, Queue<GameObject>> _poolDictionary;

    [Header("Pool Setting")]
    [SerializeField] private List<PoolData> _pools;

    protected override void Awake()
    {
        base.Awake();
        InitializePools();
    }

    //받아온 PoolData 리스트를 돌며 이름을 키값, 생성된 인스턴스를 Queue에 Enqueue
    private void InitializePools()
    {
        _poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in _pools)
        {
            //프리펩 이름을 키값으로 지정
            string key = pool.prefab.name;
            
            //키값을 가진 풀이 없으면?
            if (!_poolDictionary.ContainsKey(key))
            {
                _poolDictionary.Add(key, new Queue<GameObject>());
                for (int i = 0; i < pool.initialSize; i++)
                {
                    GameObject obj = Instantiate(pool.prefab);
                    obj.SetActive(false);
                    _poolDictionary[key].Enqueue(obj);
                }
            }
        }
    }

    //키값에 해당하는 오브젝트 꺼내기
    public GameObject GetObject(string key)
    {
        if (!_poolDictionary.ContainsKey(key))
        {
            return null;
        }
        Queue<GameObject> objectQueue = _poolDictionary[key];

        //풀에 오브젝트 남아있으면 Dequeue해서 반환
        if (objectQueue.Count > 0)
        {
            GameObject objectToSpawn = objectQueue.Dequeue();
            objectToSpawn.SetActive(true);
            return objectToSpawn;
        }
        //오브젝트 없으면 null 반환
        else
        {
            return null;
        }

    }
    //키값에 해당하는 오브젝트 돌려보내기
    public void ReturnObject(GameObject objectToReturn, string key)
    {
        //풀 도감에 없는 키값이면? 파괴
        if (!_poolDictionary.ContainsKey(key))
        {
            Destroy(objectToReturn);
            return;
        }
        //비활성화
        objectToReturn.SetActive(false);
        //키에 맞는 풀에 인스턴스 반납
        _poolDictionary[key].Enqueue(objectToReturn);
    }
}
