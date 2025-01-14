using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    #region Player Info
    [field: Header("Player Info")]
    [field: SerializeField] public PlayerEquipment Equipment { get; set; }
    public int PlayerID { get; set; }
    public PlayerStatus Status { get; private set; }
    public int Level { get; set; }
    public int Kill { get; set; }
    public int CurExp { get; set; }
    [field:SerializeField] public int[] NextExp { get; private set; } = { 3, 5, 10, 15, 20, 35, 55, 70, 100, 135, 180, 230 };

    [SerializeField] AudioManager.Sfx sfxHitSound = AudioManager.Sfx.Cancel;
    [SerializeField] float[] sfxTimer = { 0, 0.3f };
    #endregion

    public Vector2 MoveDirection { get; private set; } = Vector2.zero;

    public Scanner Scanner { get; private set; }

    PlayerAnimator animator;

    private void Awake()
    {
        if(animator == null) animator = GetComponent<PlayerAnimator>();
        if(Scanner == null) Scanner = GetComponent<Scanner>();
        if (Equipment == null) Equipment = GetComponentInChildren<PlayerEquipment>();
        Status = Equipment.Status;
    }

    private void OnEnable()
    {
        Init();
    }


    public void Init()
    {
        Equipment.Init();
        Status.CurHealth = Status.MaxHealth;
        animator.SetAnimatorController(PlayerID);
    }

    public void AddExp(int exp)
    {
        if (GameManager.Instance.IsPause) return;

        CurExp += exp;

        var curLevel = Mathf.Min(Level, NextExp.Length - 1);
        if (CurExp >= NextExp[curLevel])
        {
            CurExp -= NextExp[curLevel];
            ++Level;
            GameManager.Instance.LevelUp();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPause) return;
        
        if (collision.gameObject.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Enemy)))
        {   // 적과 충돌했을 경우
            if (collision.gameObject.TryGetComponent<IAttackAble>(out var enemy))
            {   // 초당 적 데미지만큼 감소
                Hit(enemy.GetDamage() * Time.deltaTime);
            }
        }

        if(Status.CurHealth < 0)
        {   // 체력이 없을 경우 사망
            Dead();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GameManager.Instance.IsPause) return;

        if (collision.gameObject.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.EnemyBullet)))
        {
            if (collision.gameObject.TryGetComponent<IAttackAble>(out var bullet))
            {   // 적 불릿 데미지만큼 감소
                Hit(bullet.GetDamage());
            }
        }

        if (Status.CurHealth < 0)
        {   // 체력이 없을 경우 사망
            Dead();
        }
    }

    public void Dead()
    {
        animator.SetState(PlayerAnimator.State.Dead);
        GameManager.Instance.GameOver();
    }

    public void Hit(float Damage)
    {
        Status.CurHealth -= Damage;

        float deltaTime = GameManager.Instance.GameTime - sfxTimer[0];
        if (deltaTime >= sfxTimer[1])
        {
            AudioManager.Instance.PlaySfx(sfxHitSound);
            sfxTimer[0] = GameManager.Instance.GameTime;
        }
    }

    private void OnMove(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }

}
