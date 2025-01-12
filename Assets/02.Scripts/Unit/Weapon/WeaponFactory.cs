using UnityEngine;

public static class WeaponFactory
{
    public enum WeaponType { Orbit, TargetingGun, RandomGun }

    public static Weapon Create(WeaponType type)
    {
        Weapon weapon;
        switch (type)
        {
            case WeaponType.Orbit:
                weapon = new GameObject().AddComponent<WOrbit>();
                break;
            case WeaponType.TargetingGun:
                weapon = new GameObject().AddComponent<WTargetingGun>();
                break;
            case WeaponType.RandomGun:
                weapon = new GameObject().AddComponent<WRandomGun>();
                break;
            default:
                weapon = null;
                Debug.LogError($"WeaponFectory : NULL WeaponType {type}");
                break;
        }
        return weapon;
    }
}
