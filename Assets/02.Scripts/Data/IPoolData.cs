using UnityEngine;

public interface IPoolData
{
    public enum DataType { Player, Enemy, Bullet, Item, Exp }

    /// <summary>
    /// 데이터 종류
    /// </summary>
    public DataType Type { get; }

    /// <summary>
    /// 해당 데이터 ID (PoolManager 등에서 사용)
    /// </summary>
    public int DataID { get; }

    /// <summary>
    /// 해당 데이터 이름
    /// </summary>
    public GameObject Prefab { get; }
}
