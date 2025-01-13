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
    /// 무기 기본값 설정
    /// </summary>
    /// <param name="data">해당 무기의 데이터</param>
    /// <param name="_player">무기를 사용하는 플레이어</param>
    /// <param name="_equipment">플레이어의 장비</param>
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
    /// 무기 기본 설정
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
    /// 레벨업 했을 때 업데이트 할 내용
    /// </summary>
    protected virtual void LevelUpSetting() { }

    /// <summary>
    /// 무기 공격 실행
    /// </summary>
    public abstract void Attack();
}