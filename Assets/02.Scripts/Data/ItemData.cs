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
    /// ������ ���� (����, �нú�, �Һ� ������)
    /// </summary>
    [field:Header("Main Info")]
    
    [field: SerializeField] public ItemCategory Category { get; private set; }
    /// <summary>
    /// ������ �ִ� ����
    /// </summary>
    [field: SerializeField] public int MaxLevel { get; private set; }
    /// <summary>
    /// ������ ���� ��ȣ
    /// </summary>
    [field:SerializeField] public int ItemID { get; private set; }
    /// <summary>
    /// ������ �̸�
    /// </summary>
    [field: SerializeField] public string ItemName { get; private set; }

    /// <summary>
    /// ������ ����
    /// </summary>
    [field: TextArea(order = 3)]
    [field: SerializeField] public string ItemDescription { get; private set; }
    /// <summary>
    /// ������ ������
    /// </summary>
    [field: SerializeField] public Sprite ItemIcon { get; private set; }

}
