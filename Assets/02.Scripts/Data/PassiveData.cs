using UnityEngine;

[CreateAssetMenu(fileName = "PassiveData", menuName = "Scriptable Objects/PassiveData")]
public class PassiveData : ItemData
{
    public enum EPassiveType { Damage, MoveSpeed, AttackSpeed, Health, Penetrations }

    /// <summary>
    /// 패시브 종류. Damage, AttackSpeed 등에 따라 Value 값의 종류 결졍
    /// </summary>
    [field:SerializeField] public EPassiveType PassiveType { get; private set; }

    /// <summary>
    /// 패시브 값. PassiveType 값으로 값 종류 결정, 인덱스는 Level로 접근
    /// </summary>
    [field: SerializeField] public float[] Values { get; private set; }
}
