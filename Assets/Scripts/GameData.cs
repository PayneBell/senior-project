using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int ammoDropChance = 80;

    public static List<GameObject> LiveEnemies = new List<GameObject>();

    public static WeaponType WeaponEquipped = WeaponType.DAGGER;

    public static WeaponType EquippedMelee = WeaponType.CUTLASS;
    public static WeaponType EquippedRanged = WeaponType.BLUNDERBUSS;

    public static SlotType WeaponTypeEquipped = SlotType.MELEE;

    public static int CurrentWeaponDamage = 0;

    public static int CurrentPoints = 0;

    public enum WeaponType
    {
        DAGGER,
        CUTLASS,
        PISTOL,
        BLUNDERBUSS
    }

    public enum SlotType
    {
        MELEE,
        RANGED
    }

}
