using UnityEngine;

public class WRandomGun : Weapon
{
    float timer = 0;

    protected override void WeaponInit()
    {   // 불릿을 오브젝트 풀에 추가
        PoolManager.Instance.Add(Status.BulletData);

        name = nameof(WRandomGun) + Status.BulletDataID;
    }

    public override void Attack()
    {
        // 공격 쿨타임이 끝났으면
        if (timer * Status.AttackSpeed < 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0f;

        // Count 횟수만큼 불릿 동시에 발사
        for(int i = 0; i < Status.Count; ++i)
        {
            Fire();
        }

        // 효과음 재생
        AudioManager.Instance.PlaySfx(Status.Sfx);
    }

    void Fire()
    {
        // 360도 랜덤 방향 설정
        Quaternion rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        Vector3 direction = rotation * Vector3.up;

        // 불릿 생성
        Bullet bullet = (Bullet)PoolManager.Instance.Get(Status.BulletDataID);
        bullet.transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.up, direction));
        bullet.Init(Status, Status.Penetration, direction * Status.BulletVelocity, Status.KnockBackForce);
    }
}
