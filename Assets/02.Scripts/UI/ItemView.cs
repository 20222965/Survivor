using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    [SerializeField] ItemData data;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI textlevel;
    [SerializeField] TextMeshProUGUI textName;
    [SerializeField] TextMeshProUGUI textDescription;

    public Dictionary<int, int> ItemLevel { get; set; }

    private void Awake()
    {
        if (icon == null)
        {
            icon = GetComponentsInChildren<Image>()[1];
        }
        icon.sprite = data.Icon;
        if (textlevel == null || textName == null || textDescription == null)
        {
            TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
            textlevel = texts[0];
            textName = texts[1];
            textDescription = texts[2];
        }
    }

    public void Init(Dictionary<int, int> itemLevel) => ItemLevel = itemLevel;
    public void SetItem(ItemData data)
    {
        this.data = data;
        InitItemViewUI();
    }

    private void InitItemViewUI()
    {
        int level = ItemLevel[data.ItemID];
        icon.sprite = data.Icon;
        textName.text = data.Name;
        if (data.Category == ItemData.ItemCategory.Consumable)
        {
            textlevel.text = "<color=blue>ȹ��</color>";
        }
        else if (level == 0)
        {
            textlevel.text = "<color=orange>New!</color>";
        }
        else if(level + 1 >= data.MaxLevel)
        {
            textlevel.text = "<color=red>MAX</color>";
        }
        else
        {
            textlevel.text = "Lv." + (level + 1);
        }

        string description;
        // ������ ���� ��� ù ����
        if (data.Descriptions.Length < level + 1 || string.IsNullOrEmpty(data.Descriptions[level]))
            description = data.Descriptions[0];
        else // ������ ���� ��� �ش� ���� ����
            description = data.Descriptions[level];
        textDescription.text = description;
    }

    public void OnClick()
    {
        switch (data.Category)
        {
            case ItemData.ItemCategory.Weapon:
            case ItemData.ItemCategory.Passive:
                var equip = GameManager.Instance.Player.GetComponent<PlayerEquipment>();
                equip.LevelUp(data);
                ++ItemLevel[data.ItemID];
                break;
            case ItemData.ItemCategory.Consumable:
                ((ConsumableData)data).Use();
                break;
            default:
                break;
        }
    }
}
