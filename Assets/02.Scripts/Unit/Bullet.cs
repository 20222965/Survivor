using UnityEngine;

public class Bullet : PoolAble
{
    public float Damage { get; private set; }
    public int Penetration { get; private set; }
    public float KnockBackForce { get; private set; }

    Rigidbody2D rigid;

    public void Init(float damage, int penetration, Vector3 direction, float knockBackForce)
    {
        Damage = damage;
        Penetration = penetration;
        KnockBackForce = knockBackForce;

        if (rigid != null)
        {
            rigid.linearVelocity = direction;
        }
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || Penetration < -1 || !gameObject.activeSelf) return;

        --Penetration;

        if(Penetration < 0)
        {
            ReleaseObject();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Area") || !gameObject.activeSelf) return;

        ReleaseObject();
    }
}
