using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    /// <summary>
    /// 스폰할 적 데이터 정보
    /// </summary>
    [field: SerializeField] public Data EnemyData { get; private set; }
    /// <summary>
    /// 스폰할 적 스프라이트 번호
    /// </summary>
    [field:SerializeField] public int SpriteIdx { get; private set; }
    /// <summary>
    /// 스폰할 적 체력
    /// </summary>
    [field: SerializeField] public int Health { get; private set; }
    /// <summary>
    /// 스폰할 적 공격력
    /// </summary>
    [field: SerializeField] public float Damage { get; private set; }
    /// <summary>
    /// 스폰할 적 이동속도
    /// </summary>
    [field: SerializeField] public float Speed { get; private set; }
    /// <summary>
    /// 스폰할 적이 죽으면 드랍할 경험치
    /// </summary>
    [field: SerializeField] public int Exp { get; private set; }
    /// <summary>
    /// 스폰 최대 제한. 0일 경우 제한 없음
    /// </summary>
    [field: SerializeField] public int SpawnMaxCount { get; private set; }
    /// <summary>
    /// 스폰 쿨타임
    /// </summary>
    [field: SerializeField] public float SpawnCoolTime { get; private set; }
    /// <summary>
    /// 스폰 시작 시간(게임 상단 타이머 기준)
    /// </summary>
    [field: SerializeField] public float SpawnStartTime { get; private set; }
    /// <summary>
    /// 스폰 종료 시간. 0일 경우 계속 스폰
    /// </summary>
    [field: SerializeField] public float SpawnEndTime { get; private set; }

    /// <summary>
    /// 스폰된 적이 피격 시 효과음
    /// </summary>
    public AudioManager.Sfx SfxHit = AudioManager.Sfx.Hit;
    /// <summary>
    /// 스폰된 적이 사망 시 효과음
    /// </summary>
    public AudioManager.Sfx SfxDead = AudioManager.Sfx.Dead;
}
