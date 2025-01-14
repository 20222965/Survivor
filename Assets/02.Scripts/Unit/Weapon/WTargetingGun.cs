using System.Collections;
using UnityEngine;

public class WTargetingGun : Weapon
{
    float timer = 0;
    WaitForSecondsRealtime wait = new WaitForSecondsRealtime(0.1f);

    protected override void WeaponInit()
    {   // 불릿을 오브젝트 풀에 추가
        PoolManager.Instance.Add(Data.BulletData);

        name = nameof(WTargetingGun) + Data.BulletData.DataID;
    }

    public override void Attack()
    {
        // 공격 쿨타임이 끝났고
        if (timer * AttackSpeed < 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        // 주변에 적이 있을 경우
        if (!player.Scanner.NearestTarget) return;

        // 공격 방향을 가장 가까운 위치의 적 방향으로 설정
        timer = 0f;
        Vector3 targetPos = player.Scanner.NearestTarget.position;
        Vector3 direction = (targetPos - player.transform.position).normalized;

        // 타겟이 있었던 방향으로 Count만큼 연속으로 발사
        StartCoroutine(Fire(direction));
    }

    IEnumerator Fire(Vector3 direction)
    {
        for (int i = 0; i < Count; ++i)
        {
            Bullet bullet = (Bullet)PoolManager.Instance.Get(Data.BulletData.DataID);
            bullet.transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.up, direction));
            bullet.Init(Damage, Penetration, direction * BulletVelocity, KnockBackForce);
            AudioManager.Instance.PlaySfx(Data.Sfx);

            yield return wait;
        }
    }
}
