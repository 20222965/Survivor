using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemView : MonoBehaviour
{
    public ItemData data;
    public int level;

    Image icon;
    TextMeshProUGUI textlevel;
    TextMeshProUGUI textName;
    TextMeshProUGUI textDescription;

    private void Awake()
    {
        icon = GetComponentsInChildren<Image>()[1];
        icon.sprite = data.ItemIcon;

        TextMeshProUGUI[] texts = GetComponentsInChildren<TextMeshProUGUI>();
        textlevel = texts[0];
        textName = texts[1];
        textDescription = texts[2];

        textName.text = data.ItemName;
    }

    private void OnEnable()
    {
        if (data.Category == ItemData.ItemCategory.Consumable)
        {
            textlevel.text = "<color=blue>È¹µæ</color>";
        }
        else if (level == 0)
        {
            textlevel.text = "<color=orange>New!</color>";
        }
        else
        {
            textlevel.text = "Lv." + (level + 1);
        }

        textDescription.text = data.ItemDescription;
    }
    public void OnClick()
    {
        switch (data.Category)
        {
            case ItemData.ItemCategory.Weapon:
            case ItemData.ItemCategory.Passive:
                var equip = GameManager.Instance.Player.GetComponent<PlayerEquipment>();
                equip.LevelUp(data);
                ++level;
                break;
            case ItemData.ItemCategory.Consumable:
                ((ConsumableData)data).Use();
                break;
            default:
                break;
        }
    }
}
