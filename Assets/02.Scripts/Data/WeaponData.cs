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
    /// ���Ⱑ ����ϴ� �Ҹ� ����
    /// </summary>
    [field: Header("Bullet Prefab Data")]
    [field: SerializeField] public Data BulletData { get; private set; }
    
    /// <summary>
    /// ���� ����
    /// </summary>
    [field: Header("Weapon Data")]
    [field: SerializeField] public WeaponFactory.WeaponType Weapon { get; private set; }
    /// <summary>
    /// ������ ���� ������
    /// </summary>
    [field: SerializeField] public float[] Damages { get; private set; }
    /// <summary>
    /// ������ ���� �ӵ� (0.1 => +10%)
    /// </summary>
    [field: SerializeField] public float[] AttackSpeeds { get; private set; }
    /// <summary>
    /// ������ �Ҹ� ����
    /// </summary>
    [field: SerializeField] public int[] Counts { get; private set; }
    /// <summary>
    /// ������ �Ҹ� ���� ���� (���� ���� ��� -100 ����)
    /// </summary>
    [field: SerializeField] public int[] Penetrations { get; private set; }
    /// <summary>
    /// ���� ���� �� ȿ����
    /// </summary>
    [field: SerializeField] public AudioManager.Sfx Sfx { get; private set; }
}
