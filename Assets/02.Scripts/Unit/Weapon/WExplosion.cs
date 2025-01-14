using System.Collections;
using UnityEngine;

public class WExplosion : Weapon
{
    float timer = 0;
    WaitForSeconds wait = new WaitForSeconds(1);

    protected override void WeaponInit()
    {   // 불릿을 오브젝트 풀에 추가
        PoolManager.Instance.Add(Data.BulletData);

        name = nameof(WExplosion) + Data.BulletData.DataID;
    }

    public override void Attack()
    {
        // 공격 쿨타임이 끝났으면
        if (timer * AttackSpeed < 1f)
        {
            timer += Time.deltaTime;
            return;
        }
        timer = 0f;

        // Count 횟수만큼 불릿 동시에 발사
        for(int i = 0; i < Count; ++i)
        {
            Explosion();
        }

        // 효과음 재생
        AudioManager.Instance.PlaySfx(Data.Sfx);
    }

    void Explosion()
    {
        // 주변 -5~5 랜덤 위치 설정
        Vector3 randPosition = new Vector3(transform.position.x + Random.Range(-5f, 5f), transform.position.y + Random.Range(-5f, 5f), 0);

        // 불릿 생성
        var bullet = (Bullet)PoolManager.Instance.Get(Data.BulletData.DataID);

        // 랜덤 위치로 이동
        bullet.transform.position = randPosition;
        bullet.Init(Damage, -100, Vector3.zero, KnockBackForce);

        StartCoroutine(ExplosionEnd(bullet));
    }

    IEnumerator ExplosionEnd(PoolAble transform)
    {
        yield return wait;
        transform.ReleaseObject();  // 1초 뒤 불릿 반환
    }
}
