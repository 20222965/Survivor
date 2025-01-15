using System.Collections;
using UnityEngine;

public class Enemy : PoolAble, IGetDamageAble
{
    [SerializeField] protected Rigidbody2D target;
    [SerializeField] protected Rigidbody2D rigid;
    [SerializeField] protected Collider2D col;
    [SerializeField] protected EnemyAnimator animator;

    protected WaitForFixedUpdate wait = new WaitForFixedUpdate();

    public SpawnData Data { get; set; }
    public float Damage => Data.Damage;
    public float Health { get; set; }

    protected bool isActive = false;

    protected virtual void Awake()
    {
        if(animator == null) animator = GetComponent<EnemyAnimator>();
        if (target == null) target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
        if (col == null) col = GetComponent<Collider2D>();
    }


    private void OnEnable()
    {
        if (target == null) target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        isActive = true;
        col.enabled = true;
        rigid.simulated = true;
        animator.SetState(EnemyAnimator.State.Idle);
    }

    public void Init(SpawnData data)
    {
        Data = data;
        animator.SetAnimatorController(data.SpriteIdx);
        Health = data.Health;
    }

    private void FixedUpdate()
    {
        if (!CanAct()) return;

        Move();
    }

    protected bool CanAct()
    {
        if (GameManager.Instance.IsPause) return false; // 정지 상태면 행동하지 않음.
        if (!isActive) return false;    // 몬스터가 활성 상태가 아니면 행동하지 않음
        if (animator.IsHit()) return false;  // 몬스터가 피격 상태면 행동하지 않음

        return true;
    }

    protected void Move()
    {
        animator.SetState(EnemyAnimator.State.Idle);
        Vector2 directionVector = (target.position - rigid.position).normalized;
        Vector2 nextVector = directionVector * Data.Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        ResetVelocity();
    }

    protected void ResetVelocity()
    {
        rigid.linearVelocity = Vector2.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;
        if (!collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.PlayerBullet))) return;

        var bullet = collision.GetComponent<Bullet>();
        Health -= bullet.Damage;
        if (Health > 0)
        {
            Hit(bullet.KnockBackForce);
        }
        else
        {   // DIE
            Dead();
        }
    }

    void Hit(float knockBackForce)
    {
        if (!isActive) return;

        animator.SetState(EnemyAnimator.State.Hit);
        StartCoroutine(KnockBack(knockBackForce));
        AudioManager.Instance.PlaySfx(Data.SfxHit);
    }
    IEnumerator KnockBack(float knockBackForce)
    {
        yield return wait;
        Vector3 playerPos = GameManager.Instance.Player.transform.position;
        Vector3 directionVec = (transform.position - playerPos).normalized;
        rigid.AddForce(directionVec * knockBackForce, ForceMode2D.Impulse);
    }

    void Dead()
    {
        isActive = false;
        col.enabled = false;
        rigid.simulated = false;
        animator.SetState(EnemyAnimator.State.Dead);

        // 아이템 드롭
        ExpManager.Instance.DropExp(transform.position, Data.Exp);

        if (!GameManager.Instance.IsPause)
            AudioManager.Instance.PlaySfx(Data.SfxDead);
    }
}
