public class PlayerStatus
{
    public float CurHealth { get; set; }
    public float MaxHealth { get; set; }
    public float Damage { get; set; }
    public float MoveSpeed { get; set; }
    public float AttackSpeed { get; set; }
    public int Count { get; set; }
    public int Penetration { get; set; }

    public void Init()
    {
        CurHealth = 100f;
        MaxHealth = 100f;
        MoveSpeed = 3f * (GameManager.Instance.Player.PlayerID == 0 ? 1.1f : 1f);
        AttackSpeed = GameManager.Instance.Player.PlayerID == 1 ? 1.1f : 1f;
        Damage = GameManager.Instance.Player.PlayerID == 2 ? 1.1f : 1f;
        Count = GameManager.Instance.Player.PlayerID == 3 ? 1 : 0;
        Penetration = GameManager.Instance.Player.PlayerID == 3 ? 1 : 0;
    }
}
