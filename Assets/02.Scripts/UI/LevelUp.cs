using System;
using System.Linq;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    RectTransform rectTransform;
    ItemView[] items;

    void Awake()
    {
        UnityEngine.Random.InitState(Guid.NewGuid().GetHashCode());
        rectTransform = GetComponent<RectTransform>();
        items = GetComponentsInChildren<ItemView>(true);
    }

    /// <summary>
    /// ������ ������ ������ ǥ��
    /// </summary>
    public void Show()
    {
        Next();
        GameManager.Instance.Stop();
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
        items[index].OnClick();
    }

    /// <summary>
    /// ���� �ִ� 3�� ������ ������ Ȱ��ȭ
    /// </summary>
    void Next()
    {
        // ��� ������ ��Ȱ��ȭ
        foreach (ItemView item in items)
        {
            item.gameObject.SetActive(false);
        }

        // �Һ� ������
        var conItems = items.Where(item => 
        item.data.Category == ItemData.ItemCategory.Consumable).ToList();

        // ������ �ƴ� ����, �нú� ������
        var randItems = items.Where(item => 
        item.data.Category != ItemData.ItemCategory.Consumable 
        && item.level < item.data.MaxLevel).ToList();


        while (randItems.Count > 3)  // 3�� ���� ������ ����
        {
            randItems.RemoveAt(UnityEngine.Random.Range(0, randItems.Count - 1));
        }

        if(randItems.Count <= 2)
        {   // 2�� ������ ��� ���� �Һ� ������ �߰�
            while(randItems.Count < 3 && conItems.Count > 0)
            {
                int rand = UnityEngine.Random.Range(0, conItems.Count - 1);
                randItems.Add(conItems[rand]);
                conItems.RemoveAt(rand);
            }
        }

        foreach(var randItem in randItems)
        {   // �ִ� 3�� ������ Ȱ��ȭ
            randItem.gameObject.SetActive(true);
        }
    }
}
