using UnityEngine;

[CreateAssetMenu(fileName = "PassiveData", menuName = "Scriptable Objects/PassiveData")]
public class PassiveData : ItemData
{
    public enum EPassiveType { Damage, MoveSpeed, AttackSpeed, Health, Penetrations }

    /// <summary>
    /// �нú� ����. Damage, AttackSpeed � ���� Value ���� ���� ����
    /// </summary>
    [field:SerializeField] public EPassiveType PassiveType { get; private set; }

    /// <summary>
    /// �нú� ��. PassiveType ������ �� ���� ����, �ε����� Level�� ����
    /// </summary>
    [field: SerializeField] public float[] Values { get; private set; }
}
