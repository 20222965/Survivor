using System.Collections.Generic;
using UnityEngine;

public class WOrbit : Weapon
{
    private readonly List<Transform> orbitTransforms = new List<Transform>();

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
            Transform bulletTransform;

            if (i < orbitTransforms.Count)
            {
                bulletTransform = orbitTransforms[i];
            }
            else
            {
                bulletTransform = PoolManager.Instance.Get(Data.BulletData.DataID).transform;
                orbitTransforms.Add(bulletTransform);
            }

            bulletTransform.parent = transform;
            bulletTransform.localPosition = Vector3.zero;
            bulletTransform.rotation = Quaternion.identity;

            Vector3 rotVec = Vector3.forward * (360 * i / Count);
            bulletTransform.Rotate(rotVec);
            bulletTransform.Translate(bulletTransform.up * 1.3f, Space.World);

            var bullet = bulletTransform.GetComponent<Bullet>();

            bullet.Init(Damage, -100, Vector3.zero);
        }
    }
}
