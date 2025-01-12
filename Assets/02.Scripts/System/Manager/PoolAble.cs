using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// ������Ʈ Ǯ���� �� �ִ� Ŭ����
/// </summary>
public abstract class PoolAble : MonoBehaviour
{
    public IObjectPool<PoolAble> Pool { get; set; }

    /// <summary>
    /// ������Ʈ ��ȯ
    /// </summary>
    public void ReleaseObject()
    {
        Pool.Release(this);
    }
}
