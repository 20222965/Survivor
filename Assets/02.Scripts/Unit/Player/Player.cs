using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{

    #region Player Info
    [Header("# Player Info")]
    public int PlayerID { get; set; }
    public PlayerEquipment Equipment { get; set; }
    public PlayerStatus Status { get; private set; }
    public int Level { get; set; }
    public int Kill { get; set; }
    public int CurExp { get; set; }
    [field:SerializeField] public int[] NextExp { get; private set; } = { 3, 5, 10, 15, 20, 35, 55, 70, 100, 135, 180, 230 };
    #endregion

    public Vector2 MoveDirection { get; private set; } = Vector2.zero;

    CircleCollider2D magnet;
    public float MagnetStrength { get; set; } = 5f;
    public float MagnetRadius { get => magnet.radius; set => magnet.radius = value; }

    public Scanner Scanner { get; private set; }
    public RuntimeAnimatorController[] animatorController;

    Animator animator;
    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Scanner = GetComponent<Scanner>();

        magnet = GetComponentInChildren<CircleCollider2D>();
        Equipment = GetComponentInChildren<PlayerEquipment>();
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
        animator.runtimeAnimatorController = animatorController[PlayerID];
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

    private void LateUpdate()
    {
        if (GameManager.Instance.IsPause)
            return;

        MoveAnimation();
    }

    /// <summary>
    /// �ִϸ��̼� ���
    /// </summary>
    void MoveAnimation()
    {
        // �����̰� ������
        if (MoveDirection != Vector2.zero)
        {
            // �ִϸ��̼� Idle => Run
            animator.SetBool("isRun", true);

            // �������� �̵��ϴ� ���̸� x flip
            if (MoveDirection.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.Instance.IsPause) return;
        
        if (collision.gameObject.CompareTag("Enemy"))
        {   // ���� �浹���� ���
            if (collision.gameObject.TryGetComponent<Enemy>(out var enemy))
            {   // �ʴ� �� ��������ŭ ����
                Status.CurHealth -= enemy.GetDamage() * Time.deltaTime;
            }
        }

        if(Status.CurHealth < 0)
        {   // ü���� ���� ��� ���
            animator.SetTrigger("Dead");
            GameManager.Instance.GameOver();
        }
    }

    private void OnMove(InputValue value)
    {
        MoveDirection = value.Get<Vector2>();
    }

}
