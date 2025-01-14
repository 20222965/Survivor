using System.Collections.Generic;
using UnityEngine;

public class WOrbit : Weapon
{
    private readonly List<Bullet> orbitBullets = new List<Bullet>();

    protected override void WeaponInit()
    {   // 불릿을 오브젝트 풀에 추가
        PoolManager.Instance.Add(Data.BulletData);

        // 회전 중심
        name = nameof(WOrbit) + Data.BulletData.DataID;

        LevelUpSetting();
    }

    public override void Attack()
    {
        transform.Rotate(Vector3.back * AttackSpeed * Time.deltaTime);
    }

    protected override void LevelUpSetting()
    {
        for (int i = 0; i < Count; i++)
        {
            Bullet bullet;

            if (i < orbitBullets.Count)
            {
                bullet = orbitBullets[i];
            }
            else
            {
                bullet = (Bullet)PoolManager.Instance.Get(Data.BulletData.DataID);
                orbitBullets.Add(bullet);
            }

            bullet.transform.parent = transform;
            bullet.transform.localPosition = Vector3.zero;
            bullet.transform.rotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * (360 * i / Count);
            bullet.transform.Rotate(rotVec);
            bullet.transform.Translate(bullet.transform.up * 1.3f, Space.World);

            bullet.Init(Damage, -100, Vector3.zero, KnockBackForce);
        }
    }
}
