using UnityEngine;

/// <summary>
/// 결과 UI 표시하는 함수
/// </summary>
public class Result : MonoBehaviour
{
    public GameObject[] titles;
    public void Lose()
    {
        titles[0].SetActive(true);
        titles[1].SetActive(false);
    }
    public void Win()
    {
        titles[0].SetActive(false);
        titles[1].SetActive(true);
    }
}
