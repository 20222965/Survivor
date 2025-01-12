using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/WeaponItemData")]
public class WeaponData : ItemData
{
    public enum WeaponItem
    {
        Bullet0,
        Bullet1,
        Bullet2,
        Count
    }

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
    /// 레벨별 무기 속도 (0.1 => +10%)
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
    /// 무기 시전 시 효과음
    /// </summary>
    [field: SerializeField] public AudioManager.Sfx Sfx { get; private set; }
}
