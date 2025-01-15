using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/WeaponItemData")]
public class WeaponData : ItemData
{
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
    /// ������ ���� �ӵ� (2 => 1�ʿ� 2��)
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
    /// ���Ⱑ ����ϴ� �Ҹ��� �ӵ� (�Ҹ� linearVelocity = ���� * BulletVelocity)
    /// </summary>
    [field: SerializeField] public float BulletVelocity { get; set; } = 15f;
    /// <summary>
    /// �ǰݵ� ���� �˹�Ǵ� ��
    /// </summary>
    [field: SerializeField] public float KnockBackForce { get; set; } = 3f;
    /// <summary>
    /// ���� ���� �� ȿ����
    /// </summary>
    [field: SerializeField] public AudioManager.Sfx Sfx { get; private set; }
}
