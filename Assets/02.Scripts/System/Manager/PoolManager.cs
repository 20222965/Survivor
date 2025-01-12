using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }


    /// <summary>
    /// ������Ʈ ��ȣ�� �ش�Ǵ� Ǯ�� �����ϴ� ��ųʸ�
    /// </summary>
    private readonly Dictionary<int, IObjectPool<PoolAble>> objectPoolDictionary = new();
    /// <summary>
    /// ������Ʈ ��ȣ�� �ش�Ǵ� �������� �����ϴ� ��ųʸ�
    /// </summary>
    private readonly Dictionary<int, GameObject> prefabDictionary = new();

    private void Awake()
    {
        Instance = this;
    }

    /// <summary>
    /// ������ƮǮ���� ������Ʈ ��������
    /// </summary>
    /// <param name="dataID">������ ������ ID</param>
    /// <returns>������Ʈ ��ȯ, �ش� ������Ʈ�� �������� ���� ��� null ��ȯ</returns>
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
    /// ������Ʈ Ǯ ������ ���� �߰�
    /// </summary>
    /// <param name="objectNumber">������Ʈ ��ȣ(Key)</param>
    /// <param name="prefab">������</param>
    /// <param name="defaultCapacity">�⺻ ũ��</param>
    public void Add(IPoolData data, int defaultCapacity = 10)
    {
        // �̹� ���� ��� �߰����� ����
        if (objectPoolDictionary.ContainsKey(data.DataID)) return;

        // ������Ʈ Ǯ ����. ������Ʈ Create, Get, Release, Destory�� �ʿ��� �Լ��� ����.
        IObjectPool<PoolAble> pool = new ObjectPool<PoolAble>(
            () => CreateObject(data.Prefab, data.DataID), OnGetObject, OnReleaseObject, OnDestroyObject, defaultCapacity: defaultCapacity);

        // ������Ʈ �̸��� pool/prefab�� ��ųʸ��� ����
        objectPoolDictionary.Add(data.DataID, pool);
        prefabDictionary.Add(data.DataID, data.Prefab);
    }

    #region UnityEngine.Pool�� ������ �Լ�
    /// <summary>
    /// ������Ʈ�� ������ �� ����
    /// </summary>
    /// <returns></returns>
    private PoolAble CreateObject(GameObject prefab, int dataID)
    {
        var gameObject = Instantiate(prefab, transform).GetComponent<PoolAble>();
        gameObject.Pool = objectPoolDictionary[dataID];
        return gameObject;
    }

    /// ������Ʈ�� ���� �� ����
    private void OnGetObject(PoolAble poolAble)
    {
        poolAble.gameObject.SetActive(true);
    }

    // ������Ʈ�� ��ȯ�� �� ����
    private void OnReleaseObject(PoolAble poolAble)
    {
        poolAble.gameObject.SetActive(false);
    }

    // ������Ʈ�� Ǯ���� ������ �� ����
    private void OnDestroyObject(PoolAble poolAble)
    {
        Destroy(poolAble.gameObject);
    }
    #endregion
}
