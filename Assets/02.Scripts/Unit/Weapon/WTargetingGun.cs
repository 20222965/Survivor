using System.Collections;
using UnityEngine;

public class WTargetingGun : Weapon
{
    float timer = 0;
    WaitForSeconds wait = new WaitForSeconds(0.1f);

    protected override void WeaponInit()
    {   // 불릿을 오브젝트 풀에 추가
        PoolManager.Instance.Add(Status.BulletData);

        name = nameof(WTargetingGun) + Status.BulletDataID;
    }

    public override void Attack()
    {
        // 공격 쿨타임이 끝났고
        if (timer * Status.AttackSpeed < 1f)
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
        for (int i = 0; i < Status.Count; ++i)
        {
            Bullet bullet = (Bullet)PoolManager.Instance.Get(Status.BulletData.DataID);
            bullet.transform.SetPositionAndRotation(transform.position, Quaternion.FromToRotation(Vector3.up, direction));
            bullet.Init(Status, Status.Penetration, direction * Status.BulletVelocity, Status.KnockBackForce);
            AudioManager.Instance.PlaySfx(Status.Sfx);

            yield return wait;
        }
    }
}
