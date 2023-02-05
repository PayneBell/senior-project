using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{

    public static List<GameObject> LiveEnemies = new List<GameObject>();

    public static WeaponType WeaponEquipped = WeaponType.DAGGER;

    public enum WeaponType
    {
        DAGGER,
        CUTLASS,
        PISTOL,
        BLUNDERBUSS
    }

}
