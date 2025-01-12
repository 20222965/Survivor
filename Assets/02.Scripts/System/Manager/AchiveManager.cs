using System;
using System.Collections;
using UnityEngine;

public class AchiveManager : MonoBehaviour
{
    public GameObject[] lockCharacter;
    public GameObject[] unlockCharacter;
    public GameObject noticeUI;
    
    enum Achive { UnlockCh2,  UnlockCh3 };
    Achive[] achives;

    WaitForSecondsRealtime wait = new WaitForSecondsRealtime(5);

    void Awake()
    {
        achives = (Achive[])Enum.GetValues(typeof(Achive));
        
        if(!PlayerPrefs.HasKey("MyData"))
            Init();
    }

    void Init()
    {
        PlayerPrefs.SetInt("MyData", 1);
        foreach (var achive in achives)
        {
            PlayerPrefs.SetInt(achive.ToString(), 0);
        }
    }

    private void Start()
    {
        UnLockCharacter();
    }

    private void UnLockCharacter()
    {
        for(int i = 0; i < lockCharacter.Length; i++)
        {
            string achiveName = achives[i].ToString();
            bool isUnlock = PlayerPrefs.GetInt(achiveName) == 1;
            lockCharacter[i].SetActive(!isUnlock);
            unlockCharacter[i].SetActive(isUnlock);
        }
    }

    void LateUpdate()
    {
        foreach (var achive in achives)
        {
            CheckAchive(achive);
        }
    }

    private void CheckAchive(Achive achive)
    {
        bool isAchive = false;

        switch (achive)
        {
            case Achive.UnlockCh2:
                isAchive = GameManager.Instance.Player.Kill >= 50;
                break;
            case Achive.UnlockCh3:
                isAchive = GameManager.Instance.GameTime == GameManager.Instance.EndTime;
                break;
            default:
                break;
        }

        if (isAchive && PlayerPrefs.GetInt(achive.ToString()) == 0)
        {
            PlayerPrefs.SetInt(achive.ToString(), 1);
            StartCoroutine(NoticeRoutine());

            for (int i = 0; i < noticeUI.transform.childCount; i++)
            {
                bool isActive = i == (int)achive;
                noticeUI.transform.GetChild(i).gameObject.SetActive(isActive);
            }
        }

    }

    IEnumerator NoticeRoutine()
    {
        noticeUI.SetActive(true);
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Notice);

        yield return wait;
        noticeUI.SetActive(false);

    }
}
