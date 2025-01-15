using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/ConsumableData")]
public class ConsumableData : ItemData
{
    public enum EConsumableType
    {
        Heal
    }

    /// <summary>
    /// 소비 아이템 종류
    /// </summary>
    [field: SerializeField] public EConsumableType ConsumableType { get; set; }

    /// <summary>
    /// 소비 아이템 값
    /// </summary>
    [field: SerializeField] public int Value { get; private set; }


    /// <summary>
    /// 소비 아이템 사용
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
