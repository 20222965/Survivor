using System.Collections;
using UnityEngine;

public class Enemy : PoolAble
{

    public RuntimeAnimatorController[] animCon;
    [SerializeField] Rigidbody2D target;


    Rigidbody2D rigid;
    Collider2D col;
    Animator anim;
    SpriteRenderer spriteRenderer;
    WaitForFixedUpdate wait;

    public SpawnData Data { get; set; }
    public float Health { get; set; }

    bool isActive = false;
    bool isHit;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        wait = new WaitForFixedUpdate();
    }

    private void OnEnable()
    {
        target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        isActive = true;
        col.enabled = true;
        spriteRenderer.sortingOrder = 2;
        rigid.simulated = true;
        anim.SetBool("Dead", false);
    }
    public void Init(SpawnData data)
    {
        Data = data;
        anim.runtimeAnimatorController = animCon[data.SpriteIdx];
        Health = data.Health;
    }

    public float GetDamage()
    {
        return Data.Damage;
    }


    void FixedUpdate()
    {
        if (GameManager.Instance.IsPause) return;
        if (!isActive) return; // 몬스터가 활성 상태가 아니면 움직이지 않음.

        if (GameManager.Instance.GameTime >= GameManager.Instance.EndTime)
        {
            Dead();
            return;
        }

        if (isHit) return;   // 몬스터가 피격 상태면 움직이지 않음

        Vector2 directionVector = (target.position - rigid.position).normalized;
        Vector2 nextVector = directionVector * Data.Speed * Time.fixedDeltaTime;
        rigid.MovePosition(rigid.position + nextVector);
        rigid.linearVelocity = Vector2.zero;

    }

    private void LateUpdate()
    {
        if (GameManager.Instance.IsPause) return;
        if (!isActive) return;

        spriteRenderer.flipX = target.position.x < rigid.position.x;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActive) return;
        if (!collision.CompareTag("PlayerBullet")) return;

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


    void HitStart()
    {
        isHit = true;
    }
    void HitEnd()
    {
        isHit = false;
    }

    void Hit(float knockBackForce)
    {
        if (!isActive) return;

        anim.SetTrigger("Hit");
        StartCoroutine(KnockBack(knockBackForce));
        AudioManager.Instance.PlaySfx(AudioManager.Sfx.Hit);
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
        spriteRenderer.sortingOrder = 1;
        rigid.simulated = false;
        anim.SetBool("Dead", true);
        
        // 아이템 드롭
        ExpManager.Instance.DropExp(transform.position, Data.Exp);

        if(!GameManager.Instance.IsPause)
            AudioManager.Instance.PlaySfx(Data.Sfx);
    }
}
