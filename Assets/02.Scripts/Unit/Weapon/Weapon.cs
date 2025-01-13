using UnityEngine;

public abstract class Weapon : MonoBehaviour
{

    public WeaponData Data { get; set; }

    public int Level { get; set; }

    public float Damage
    {
        get
        {
            if (Data.Damages.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.Damages.Length) - 1;
                return Data.Damages[idx] * equip.Status.Damage;
            }
            return equip.Status.Damage;
        }
    }
    public float AttackSpeed
    {
        get
        {
            if (Data.AttackSpeeds.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.AttackSpeeds.Length) - 1;
                return (1 + Data.AttackSpeeds[idx]) * equip.Status.AttackSpeed;
            }
            else
            {
                return equip.Status.AttackSpeed;
            }
        }
    }
    public int Count
    {
        get
        {
            if (Data.Counts.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.Counts.Length) - 1;
                return equip.Status.Count + Data.Counts[idx];
            }
            return equip.Status.Count;
        }
    }
    public int Penetration
    {
        get
        {
            if (Data.Penetrations.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.Penetrations.Length) - 1;
                return equip.Status.Penetration + Data.Penetrations[idx];
            }
            return equip.Status.Penetration;
        }
    }
    public float BulletVelocity
    {
        get
        {
            return Data.BulletVelocity;
        }
    }
    public float KnockBackForce
    {
        get
        {
            return Data.KnockBackForce;
        }
    }

    protected PlayerEquipment equip;
    protected Player player;

    /// <summary>
    /// ���� �⺻�� ����
    /// </summary>
    /// <param name="data">�ش� ������ ������</param>
    /// <param name="_player">���⸦ ����ϴ� �÷��̾�</param>
    /// <param name="_equipment">�÷��̾��� ���</param>
    public virtual void Init(WeaponData data, Player _player, PlayerEquipment _equipment)
    {
        Data = data;
        player = _player;
        equip = _equipment;

        transform.parent = _player.transform;
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        Level = 1;

        WeaponInit();
    }
    public virtual void Init(WeaponData data)
    {
        Init(data, GameManager.Instance.Player, GameManager.Instance.Player.GetComponent<PlayerEquipment>());
    }

    /// <summary>
    /// ���� �⺻ ����
    /// </summary>
    protected abstract void WeaponInit();

    public void LevelUp()
    {
        if (Level < Data.MaxLevel)
        {
            ++Level;
            LevelUpSetting();
        }
    }

    /// <summary>
    /// ������ ���� �� ������Ʈ �� ����
    /// </summary>
    protected virtual void LevelUpSetting() { }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public abstract void Attack();
}