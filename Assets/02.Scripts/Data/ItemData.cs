using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/ItemData")]
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
    [field: SerializeField] public string Name { get; private set; }

    /// <summary>
    /// ������ ����
    /// </summary>
    [field: TextArea(order = 3)]
    [field: SerializeField] public string[] Descriptions { get; private set; } = new string[1];
    /// <summary>
    /// ������ ������
    /// </summary>
    [field: SerializeField] public Sprite Icon { get; private set; }

}
