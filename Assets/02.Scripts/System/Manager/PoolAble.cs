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
        if(IsActive())
            Pool.Release(this);
    }

    /// <summary>
    /// 활성 상태인지 확인
    /// </summary>
    /// <returns>true : 활성 상태, false : 이미 반환된 개체</returns>
    public bool IsActive() => gameObject.activeSelf;
}
