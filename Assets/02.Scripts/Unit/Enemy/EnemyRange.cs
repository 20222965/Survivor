using System.Collections;
using UnityEngine;

public class EnemyRange : Enemy
{
    [field:SerializeField] public Data BulletData { get; set; }
    [field: SerializeField] public float AttackDistance { get; set; } = 6f;
    [field: SerializeField] public float AttackSpeed { get; set; } = 0.3f;
    [field: SerializeField] public float BulletSpeed { get; set; } = 2.5f;

    float timer = 0f;

    protected override void Awake()
    {
        base.Awake();
        PoolManager.Instance.Add(BulletData);
    }

    private void OnEnable()
    {
        if (target == null) target = GameManager.Instance.Player.GetComponent<Rigidbody2D>();
        isActive = true;
        col.enabled = true;
        rigid.simulated = true;
        animator.SetState(EnemyAnimator.State.Idle);
    }


    private void FixedUpdate()
    {
        if (!CanAct()) return;

        float distance = Vector2.Distance(target.position, rigid.position);
        if (distance <= AttackDistance)
        {
            Attack();
        }
        else
        {
            Move();
        }

    }

    void Attack()
    {
        // 공격 쿨타임이 끝났으면
        float deltaTime = GameManager.Instance.GameTime - timer;
        if (deltaTime * AttackSpeed < 1f)
        {
            return;
        }
        timer = GameManager.Instance.GameTime;


        // 타겟이 있었던 방향으로 발사
        Vector3 direction = (target.position - rigid.position).normalized;

        Bullet bullet = (Bullet)PoolManager.Instance.Get(BulletData.DataID);
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.up, direction));
        bullet.Init(Data.Damage, 0, direction * BulletSpeed, 0f, TagAndLayer.Tag.Player);
    }

}
