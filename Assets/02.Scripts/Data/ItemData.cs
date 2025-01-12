using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/ItemData")]
public class ItemData : ScriptableObject
{
    public enum ItemCategory
    {
        Weapon,
        Passive,
        Consumable
    }
    /// <summary>
    /// 아이템 종류 (무기, 패시브, 소비 아이템)
    /// </summary>
    [field:Header("Main Info")]
    
    [field: SerializeField] public ItemCategory Category { get; private set; }
    /// <summary>
    /// 아이템 최대 레벨
    /// </summary>
    [field: SerializeField] public int MaxLevel { get; private set; }
    /// <summary>
    /// 아이템 고유 번호
    /// </summary>
    [field:SerializeField] public int ItemID { get; private set; }
    /// <summary>
    /// 아이템 이름
    /// </summary>
    [field: SerializeField] public string ItemName { get; private set; }

    /// <summary>
    /// 아이템 설명
    /// </summary>
    [field: TextArea(order = 3)]
    [field: SerializeField] public string ItemDescription { get; private set; }
    /// <summary>
    /// 아이템 아이콘
    /// </summary>
    [field: SerializeField] public Sprite ItemIcon { get; private set; }

}
