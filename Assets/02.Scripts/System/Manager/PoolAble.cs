using UnityEngine;
using UnityEngine.Pool;

/// <summary>
/// 오브젝트 풀링할 수 있는 클래스
/// </summary>
public abstract class PoolAble : MonoBehaviour
{
    public IObjectPool<PoolAble> Pool { get; set; }

    /// <summary>
    /// 오브젝트 반환
    /// </summary>
    public void ReleaseObject()
    {
        Pool.Release(this);
    }
}
