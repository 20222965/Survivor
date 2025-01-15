using UnityEngine;

public class WeaponStatus : IGetDamageAble
{
    public WeaponStatus(WeaponData data, PlayerEquipment equipment, int level = 1)
    {
        Data = data;
        Equipment = equipment;
        Level = level;
    }
    public void LevelUp()
    {
        if (Level < Data.MaxLevel)
        {
            ++Level;
        }
    }

    public WeaponData Data { get; set; }
    public PlayerEquipment Equipment { get; set; }

    public int Level { get; set; }

    public Data BulletData => Data.BulletData;
    public int BulletDataID => Data.BulletData.DataID;

    public float Damage
    {
        get
        {
            if (Data.Damages.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.Damages.Length) - 1;
                return Data.Damages[idx] * Equipment.Status.Damage;
            }
            return Equipment.Status.Damage;
        }
    }
    public float AttackSpeed
    {
        get
        {
            if (Data.AttackSpeeds.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.AttackSpeeds.Length) - 1;
                return Data.AttackSpeeds[idx] * Equipment.Status.AttackSpeed;
            }
            else
            {
                return Equipment.Status.AttackSpeed;
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
                return Equipment.Status.Count + Data.Counts[idx];
            }
            return Equipment.Status.Count;
        }
    }
    public int Penetration
    {
        get
        {
            if (Data.Penetrations.Length > 0)
            {
                int idx = Mathf.Min(Level, Data.Penetrations.Length) - 1;
                return Equipment.Status.Penetration + Data.Penetrations[idx];
            }
            return Equipment.Status.Penetration;
        }
    }
    public float BulletVelocity => Data.BulletVelocity;
    public float KnockBackForce => Data.KnockBackForce;

    public AudioManager.Sfx Sfx => Data.Sfx;

    public bool CompareItem(ItemData data) => Data.ItemID == data.ItemID;
}
