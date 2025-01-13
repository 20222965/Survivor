using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipment : MonoBehaviour
{
    PlayerStatus basic = new PlayerStatus();
    List<PassiveStatus> passives = new List<PassiveStatus>();
    List<Weapon> weapons = new List<Weapon>();
    public PlayerStatus Status { get; set; } = new PlayerStatus();

    public void Init()
    {
        basic.Init();
        Status.Init();
    }

    private void Awake()
    {
        Init();
    }

    private void Update()
    {
        if (GameManager.Instance.IsPause) return;

        foreach (var weapon in weapons)
        {
            weapon.Attack();
        }
    }

    public void LevelUp(ItemData data)
    {
        if (data.Category == ItemData.ItemCategory.Passive)
        {
            LevelUpPassive((PassiveData)data);
        }

        else if (data.Category == ItemData.ItemCategory.Weapon)
        {
            LevelUpWeapon((WeaponData)data);
        }
    }

    public void LevelUpPassive(PassiveData data)
    {
        for (int i = 0; i < passives.Count; ++i)
        {
            var passive = passives[i];
            if (passive.Data.ItemID == data.ItemID)
            {
                passive.LevelUp();

                switch (passive.Data.PassiveType)
                {
                    case PassiveData.EPassiveType.Damage:
                        Status.Damage = basic.Damage * (1 + passive.GetValue());
                        break;
                    case PassiveData.EPassiveType.MoveSpeed:
                        Status.MoveSpeed = basic.MoveSpeed * (1 + passive.GetValue());
                        break;
                    case PassiveData.EPassiveType.AttackSpeed:
                        Status.AttackSpeed = basic.AttackSpeed * (1 + passive.GetValue());
                        break;
                    case PassiveData.EPassiveType.Health:
                        Status.MaxHealth = basic.MaxHealth + passive.GetValue();
                        break;
                    default:
                        break;
                }
                return;
            }
        }
        passives.Add(new PassiveStatus(data));
    }

    public void LevelUpWeapon(WeaponData data)
    {
        for (int i = 0; i < weapons.Count; ++i)
        {
            if (weapons[i].Data.ItemID == data.ItemID)
            {
                weapons[i].LevelUp();
                return;
            }
        }

        var weapon = WeaponFactory.Create(data.Weapon);
        weapon.Init(data);
        weapons.Add(weapon);
    }

}
