using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Bullet : PoolAble, IAttackAble
{

    public float Damage { get; private set; }
    public int Penetration { get; private set; }
    public float KnockBackForce { get; private set; }

    [SerializeField] Rigidbody2D rigid;
    TagAndLayer.Tag targetTag;

    void Awake()
    {
        if (rigid == null) rigid = GetComponent<Rigidbody2D>();
    }

    public void Init(float damage, int penetration, Vector3 direction, float knockBackForce, TagAndLayer.Tag targetTag = TagAndLayer.Tag.Enemy)
    {
        Damage = damage;
        Penetration = penetration;
        KnockBackForce = knockBackForce;
        this.targetTag = targetTag;

        if (rigid != null)
        {
            rigid.linearVelocity = direction;
        }
    }

    public float GetDamage() => Damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(TagAndLayer.GetTag(targetTag)) || Penetration < -1 || !gameObject.activeSelf) return;

        --Penetration;

        if(Penetration < 0)
        {
            ReleaseObject();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag(TagAndLayer.GetTag(TagAndLayer.Tag.Area)) || !gameObject.activeSelf) return;

        ReleaseObject();
    }
}
