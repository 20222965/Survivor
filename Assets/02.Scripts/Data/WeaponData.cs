using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/WeaponItemData")]
public class WeaponData : ItemData
{
    /// <summary>
    /// 무기가 사용하는 불릿 정보
    /// </summary>
    [field: Header("Bullet Prefab Data")]
    [field: SerializeField] public Data BulletData { get; private set; }
    
    /// <summary>
    /// 무기 종류
    /// </summary>
    [field: Header("Weapon Data")]
    [field: SerializeField] public WeaponFactory.WeaponType Weapon { get; private set; }
    /// <summary>
    /// 레벨별 무기 데미지
    /// </summary>
    [field: SerializeField] public float[] Damages { get; private set; }
    /// <summary>
    /// 레벨별 무기 속도 (2 => 1초에 2번)
    /// </summary>
    [field: SerializeField] public float[] AttackSpeeds { get; private set; }
    /// <summary>
    /// 레벨별 불릿 개수
    /// </summary>
    [field: SerializeField] public int[] Counts { get; private set; }
    /// <summary>
    /// 레벨별 불릿 관통 제한 (제한 없을 경우 -100 권장)
    /// </summary>
    [field: SerializeField] public int[] Penetrations { get; private set; }
    /// <summary>
    /// 무기가 사용하는 불릿의 속도 (불릿 linearVelocity = 방향 * BulletVelocity)
    /// </summary>
    [field: SerializeField] public float BulletVelocity { get; set; } = 15f;
    /// <summary>
    /// 피격된 적이 넉백되는 힘
    /// </summary>
    [field: SerializeField] public float KnockBackForce { get; set; } = 3f;
    /// <summary>
    /// 무기 시전 시 효과음
    /// </summary>
    [field: SerializeField] public AudioManager.Sfx Sfx { get; private set; }
}
