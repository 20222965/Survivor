using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    /// <summary>
    /// HUD 종류
    /// </summary>
    public enum InfoType
    {
        Exp,
        Level,
        Kill,
        Time,
        Health
    }
    public InfoType type;

    TextMeshProUGUI myText;
    Slider mySlider;
    Player player;

    private void Awake()
    {
        myText = GetComponent<TextMeshProUGUI>();
        mySlider = GetComponent<Slider>();
        player = GameManager.Instance.Player;
    }

    private void LateUpdate()
    {
        switch (type)
        {
            case InfoType.Exp:      // 플레이어 경험치바 표시
                var curLevel = Mathf.Min(player.Level, player.NextExp.Length - 1);
                float curExp = player.CurExp;
                float maxExp = player.NextExp[curLevel];
                mySlider.value = curExp / maxExp;
                break;
            case InfoType.Level:    // 플레이어 레벨 표시
                myText.text = "Lv." + player.Level;
                break;
            case InfoType.Kill:     // 킬 표시
                myText.text = player.Kill.ToString();
                break;
            case InfoType.Time:     // 플레이한 시간 표시
                int curTime = (int)GameManager.Instance.GameTime;
                myText.text = string.Format("{0:D2}:{1:D2}", curTime / 60, curTime % 60);
                break;
            case InfoType.Health:   // 플레이어 체력 표시
                float curhealth = player.Status.CurHealth;
                float maxHealth = player.Status.MaxHealth;
                mySlider.value = curhealth / maxHealth;
                break;
            default:
                break;
        }
    }

}
