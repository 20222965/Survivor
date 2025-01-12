using UnityEngine;

public interface IPoolData
{
    public enum DataType { Player, Enemy, Bullet, Item, Exp }

    /// <summary>
    /// ������ ����
    /// </summary>
    public DataType Type { get; }

    /// <summary>
    /// �ش� ������ ID (PoolManager ��� ���)
    /// </summary>
    public int DataID { get; }

    /// <summary>
    /// �ش� ������ �̸�
    /// </summary>
    public GameObject Prefab { get; }
}
