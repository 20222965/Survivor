using System.Collections.Generic;
using UnityEngine;

public class WOrbit : Weapon
{
    private readonly List<Bullet> orbitBullets = new List<Bullet>();

    protected override void WeaponInit()
    {   // 불릿을 오브젝트 풀에 추가
        PoolManager.Instance.Add(Status.BulletData);

        // 회전 중심
        name = nameof(WOrbit) + Status.BulletDataID;

        LevelUpSetting();
    }

    public override void Attack()
    {
        transform.Rotate(Vector3.back * Status.AttackSpeed * Time.deltaTime);
    }

    protected override void LevelUpSetting()
    {
        for (int i = 0; i < Status.Count; i++)
        {
            Bullet bullet;

            if (i < orbitBullets.Count)
            {
                bullet = orbitBullets[i];
            }
            else
            {
                bullet = (Bullet)PoolManager.Instance.Get(Status.BulletDataID);
                orbitBullets.Add(bullet);
            }

            bullet.transform.parent = transform;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.rotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * (360 * i / Status.Count);
            bullet.transform.Rotate(rotVec);
            bullet.transform.Translate(bullet.transform.up * 1.3f, Space.World);

            bullet.Init(Status, -100, Vector3.zero, Status.KnockBackForce);
        }
    }
}
