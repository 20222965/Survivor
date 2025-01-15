using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelUpHandler : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] ItemView[] itemViews;
    [SerializeField] ItemData[] itemDatas;
    Dictionary<int, int> ItemLevel { get; set; } = new Dictionary<int, int>();

    void Awake()
    {
        UnityEngine.Random.InitState(Guid.NewGuid().GetHashCode());
        rectTransform = GetComponent<RectTransform>();
        if (itemViews == null) itemViews = GetComponentsInChildren<ItemView>(true);
        
        foreach(var itemData in itemDatas)
        {
            ItemLevel[itemData.ItemID] = 0;
        }
        for(int i = 0; i < itemViews.Length; i++)
        {
            itemViews[i].Init(ItemLevel);
        }
    }

    /// <summary>
    /// ������ ������ ������ ǥ��
    /// </summary>
    public void Show()
    {
        GameManager.Instance.Stop();
        RandomItemViewSetting();
        rectTransform.localScale = Vector3.one;

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.LevelUp);
    }

    public void Hide()
    {
        rectTransform.localScale = Vector3.zero;
        GameManager.Instance.Resume();

        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Select);
    }

    public void Select(int index)
    {
        var item = itemDatas.First(itemData => itemData.ItemID == index);
        itemViews[0].SetItem(item);
        itemViews[0].OnClick();
    }

    /// <summary>
    /// ���� �ִ� 3�� ������ ������ Ȱ��ȭ
    /// </summary>
    void RandomItemViewSetting()
    {
        // ��� ������ ��Ȱ��ȭ
        foreach (ItemView item in itemViews)
        {
            item.gameObject.SetActive(false);
        }

        // �Һ� ������
        var conItems = itemDatas.Where(itemData =>
        itemData.Category == ItemData.ItemCategory.Consumable).ToList();

        // ������ �ƴ� ����, �нú� ������
        var randItems = itemDatas.Where(itemData =>
        itemData.Category != ItemData.ItemCategory.Consumable && ItemLevel[itemData.ItemID] < itemData.MaxLevel).ToList();

        while (randItems.Count > 3)  // 3�� ���� ������ ����
        {
            randItems.RemoveAt(UnityEngine.Random.Range(0, randItems.Count - 1));
        }

        // 2�� ������ ��� ���� �Һ� ������ �߰�
        while (randItems.Count < 3 && conItems.Count > 0)
        {
            int rand = UnityEngine.Random.Range(0, conItems.Count - 1);
            randItems.Add(conItems[rand]);
            conItems.RemoveAt(rand);
        }

        for (int i = 0; i < randItems.Count; i++)
        {   // �ִ� 3�� ������ Ȱ��ȭ
            itemViews[i].SetItem(randItems[i]);
            itemViews[i].gameObject.SetActive(true);
        }

        //// �Һ� ������
        //var conItems = itemViews.Where(item => 
        //item.data.Category == ItemData.ItemCategory.Consumable).ToList();

        //// ������ �ƴ� ����, �нú� ������
        //var randItems = itemViews.Where(item => 
        //item.data.Category != ItemData.ItemCategory.Consumable 
        //&& item.level < item.data.MaxLevel).ToList();


        //while (randItems.Count > 3)  // 3�� ���� ������ ����
        //{
        //    randItems.RemoveAt(UnityEngine.Random.Range(0, randItems.Count - 1));
        //}

        //if(randItems.Count <= 2)
        //{   // 2�� ������ ��� ���� �Һ� ������ �߰�
        //    while(randItems.Count < 3 && conItems.Count > 0)
        //    {
        //        int rand = UnityEngine.Random.Range(0, conItems.Count - 1);
        //        randItems.Add(conItems[rand]);
        //        conItems.RemoveAt(rand);
        //    }
        //}

        //foreach(var randItem in randItems)
        //{   // �ִ� 3�� ������ Ȱ��ȭ
        //    randItem.gameObject.SetActive(true);
        //}
    }
}
