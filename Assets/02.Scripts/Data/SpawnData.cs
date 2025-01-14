using UnityEngine;

[CreateAssetMenu(fileName = "SpawnData", menuName = "Scriptable Objects/SpawnData")]
public class SpawnData : ScriptableObject
{
    /// <summary>
    /// ������ �� ������ ����
    /// </summary>
    [field: SerializeField] public Data EnemyData { get; private set; }
    /// <summary>
    /// ������ �� ��������Ʈ ��ȣ
    /// </summary>
    [field:SerializeField] public int SpriteIdx { get; private set; }
    /// <summary>
    /// ������ �� ü��
    /// </summary>
    [field: SerializeField] public int Health { get; private set; }
    /// <summary>
    /// ������ �� ���ݷ�
    /// </summary>
    [field: SerializeField] public float Damage { get; private set; }
    /// <summary>
    /// ������ �� �̵��ӵ�
    /// </summary>
    [field: SerializeField] public float Speed { get; private set; }
    /// <summary>
    /// ������ ���� ������ ����� ����ġ
    /// </summary>
    [field: SerializeField] public int Exp { get; private set; }
    /// <summary>
    /// ���� �ִ� ����. 0�� ��� ���� ����
    /// </summary>
    [field: SerializeField] public int SpawnMaxCount { get; private set; }
    /// <summary>
    /// ���� ��Ÿ��
    /// </summary>
    [field: SerializeField] public float SpawnCoolTime { get; private set; }
    /// <summary>
    /// ���� ���� �ð�(���� ��� Ÿ�̸� ����)
    /// </summary>
    [field: SerializeField] public float SpawnStartTime { get; private set; }
    /// <summary>
    /// ���� ���� �ð�. 0�� ��� ��� ����
    /// </summary>
    [field: SerializeField] public float SpawnEndTime { get; private set; }

    /// <summary>
    /// ������ ���� �ǰ� �� ȿ����
    /// </summary>
    public AudioManager.Sfx SfxHit = AudioManager.Sfx.Hit;
    /// <summary>
    /// ������ ���� ��� �� ȿ����
    /// </summary>
    public AudioManager.Sfx SfxDead = AudioManager.Sfx.Dead;
}
