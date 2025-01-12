using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }


    /// <summary>
    /// 오브젝트 번호에 해당되는 풀을 저장하는 딕셔너리
    /// </summary>
    private readonly Dictionary<int, IObjectPool<PoolAble>> objectPoolDictionary = new();
    /// <summary>
    /// 오브젝트 번호에 해당되는 프리팹을 저장하는 딕셔너리
    /// </summary>
    private readonly Dictionary<int, GameObject> prefabDictionary = new();

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// 오브젝트풀에서 오브젝트 가져오기
    /// </summary>
    /// <param name="dataID">가져올 데이터 ID</param>
    /// <returns>오브젝트 반환, 해당 오브젝트가 존재하지 않을 경우 null 반환</returns>
    public PoolAble Get(int dataID)
    {
        if (!objectPoolDictionary.TryGetValue(dataID, out var pool))
        {
            Debug.Log($"PoolManager. NULL {dataID}");
            return null;
        }

        return pool.Get();
    }

    /// <summary>
    /// 오브젝트 풀 프리팹 종류 추가
    /// </summary>
    /// <param name="objectNumber">오브젝트 번호(Key)</param>
    /// <param name="prefab">프리팹</param>
    /// <param name="defaultCapacity">기본 크기</param>
    public void Add(IPoolData data, int defaultCapacity = 10)
    {
        // 이미 있을 경우 추가하지 않음
        if (objectPoolDictionary.ContainsKey(data.DataID)) return;

        // 오브젝트 풀 생성. 오브젝트 Create, Get, Release, Destory에 필요한 함수를 전달.
        IObjectPool<PoolAble> pool = new ObjectPool<PoolAble>(
            () => CreateObject(data.Prefab, data.DataID), OnGetObject, OnReleaseObject, OnDestroyObject, defaultCapacity: defaultCapacity);

        // 오브젝트 이름과 pool/prefab을 딕셔너리로 연결
        objectPoolDictionary.Add(data.DataID, pool);
        prefabDictionary.Add(data.DataID, data.Prefab);
    }

    #region UnityEngine.Pool에 전달할 함수
    /// <summary>
    /// 오브젝트가 생성될 때 실행
    /// </summary>
    /// <returns></returns>
    private PoolAble CreateObject(GameObject prefab, int dataID)
    {
        var gameObject = Instantiate(prefab, transform).GetComponent<PoolAble>();
        gameObject.Pool = objectPoolDictionary[dataID];
        return gameObject;
    }

    /// 오브젝트를 꺼낼 때 실행
    private void OnGetObject(PoolAble poolAble)
    {
        poolAble.gameObject.SetActive(true);
    }

    // 오브젝트를 반환할 때 실행
    private void OnReleaseObject(PoolAble poolAble)
    {
        poolAble.gameObject.SetActive(false);
    }

    // 오브젝트를 풀에서 제거할 때 실행
    private void OnDestroyObject(PoolAble poolAble)
    {
        Destroy(poolAble.gameObject);
    }
    #endregion
}
