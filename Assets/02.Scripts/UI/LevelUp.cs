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
    /// 레벨업 아이템 선택지 표시
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
    /// 랜덤 최대 3개 아이템 선택지 활성화
    /// </summary>
    void Next()
    {
        // 모든 아이템 비활성화
        foreach (ItemView item in items)
        {
            item.gameObject.SetActive(false);
        }

        // 소비 아이템
        var conItems = items.Where(item => 
        item.data.Category == ItemData.ItemCategory.Consumable).ToList();

        // 만렙이 아닌 무기, 패시브 아이템
        var randItems = items.Where(item => 
        item.data.Category != ItemData.ItemCategory.Consumable 
        && item.level < item.data.MaxLevel).ToList();


        while (randItems.Count > 3)  // 3개 남을 때까지 제거
        {
            randItems.RemoveAt(UnityEngine.Random.Range(0, randItems.Count - 1));
        }

        if(randItems.Count <= 2)
        {   // 2개 이하일 경우 랜덤 소비 아이템 추가
            while(randItems.Count < 3 && conItems.Count > 0)
            {
                int rand = UnityEngine.Random.Range(0, conItems.Count - 1);
                randItems.Add(conItems[rand]);
                conItems.RemoveAt(rand);
            }
        }

        foreach(var randItem in randItems)
        {   // 최대 3개 아이템 활성화
            randItem.gameObject.SetActive(true);
        }
    }
}
