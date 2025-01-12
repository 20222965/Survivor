using UnityEngine;

public class Bullet : PoolAble
{
    public float damage;
    public int penetration;

    Rigidbody2D rigid;

    public void Init(float damage, int penetration, Vector3 direction)
    {
        this.damage = damage;
        this.penetration = penetration;

        if(penetration >= 0)
        {
            rigid.linearVelocity = direction * 15f;
        }
    }
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Enemy") || penetration < -1 || !gameObject.activeSelf) return;

        --penetration;

        if(penetration < 0)
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
