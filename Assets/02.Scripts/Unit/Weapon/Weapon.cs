using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    /// <summary>
    /// ���� ���� �� ���� ����
    /// </summary>
    public WeaponStatus Status { get; set; }
    protected Player player;

    /// <summary>
    /// ���� �⺻�� ����
    /// </summary>
    /// <param name="data">�ش� ������ ������</param>
    /// <param name="_player">���⸦ ����ϴ� �÷��̾�</param>
    /// <param name="_equipment">�÷��̾��� ���</param>
    public virtual void Init(WeaponData data, Player _player, PlayerEquipment _equipment)
    {
        Status = new WeaponStatus(data, _equipment);

        player = _player;
        transform.parent = _player.transform;
        transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);

        WeaponInit();
    }
    public virtual void Init(WeaponData data)
    {
        Init(data, GameManager.Instance.Player, GameManager.Instance.Player.GetComponent<PlayerEquipment>());
    }

    /// <summary>
    /// ���� ���������� Ȯ��
    /// </summary>
    /// <param name="item">Ȯ���� ������</param>
    /// <returns>true: ���� ������, false: �ٸ� ������</returns>
    public bool CompareItem(ItemData item) => Status.CompareItem(item);


    /// <summary>
    /// ���� ������
    /// </summary>
    public virtual void LevelUp()
    {
        Status.LevelUp();
        LevelUpSetting();
    }

    /// <summary>
    /// ���� �⺻ ����
    /// </summary>
    protected abstract void WeaponInit();

    /// <summary>
    /// ������ ���� �� ������Ʈ �� ����
    /// </summary>
    protected virtual void LevelUpSetting() { }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public abstract void Attack();
}