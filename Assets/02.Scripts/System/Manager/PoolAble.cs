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
        if(IsActive())
            Pool.Release(this);
    }

    /// <summary>
    /// Ȱ�� �������� Ȯ��
    /// </summary>
    /// <returns>true : Ȱ�� ����, false : �̹� ��ȯ�� ��ü</returns>
    public bool IsActive() => gameObject.activeSelf;
}
