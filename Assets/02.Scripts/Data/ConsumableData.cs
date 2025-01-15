using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/ConsumableData")]
public class ConsumableData : ItemData
{
    public enum EConsumableType
    {
        Heal
    }

    /// <summary>
    /// �Һ� ������ ����
    /// </summary>
    [field: SerializeField] public EConsumableType ConsumableType { get; set; }

    /// <summary>
    /// �Һ� ������ ��
    /// </summary>
    [field: SerializeField] public int Value { get; private set; }


    /// <summary>
    /// �Һ� ������ ���
    /// </summary>
    public void Use()
    {
        switch (ConsumableType)
        {
            case EConsumableType.Heal:
                GameManager.Instance.Player.Status.CurHealth = GameManager.Instance.Player.Status.MaxHealth * Value;
                break;
            default:
                break;
        }
    }
}
