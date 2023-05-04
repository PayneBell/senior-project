using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public static int ammoDropChance = 80;

    public static List<GameObject> LiveEnemies = new List<GameObject>();
    public static List<GameObject> LiveSpawners = new List<GameObject>();

    public static WeaponType WeaponEquipped = WeaponType.NONE;

    public static WeaponType EquippedMelee = WeaponType.NONE;
    public static WeaponType EquippedRanged = WeaponType.NONE;

    public static SlotType WeaponTypeEquipped = SlotType.MELEE;

    public static int CurrentWeaponDamage = 0;

    public static int CurrentPoints = 0;

    public static int CurrentWave = 1;

    public static int BaseMeleeEnemyHealth = 3;
    public static int BaseRangedEnemyHealth = 5;

    public enum WeaponType
    {
        DAGGER,
        CUTLASS,
        PISTOL,
        BLUNDERBUSS,
        NONE
    }

    public enum SlotType
    {
        MELEE,
        RANGED
    }

}
