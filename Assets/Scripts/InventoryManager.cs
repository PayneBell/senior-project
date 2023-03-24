using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public int daggerLevel = 1;
    public int swordLevel = 1;
    public int pistolLevel = 1;
    public int shotgunLevel = 1;

    public int daggerDmg;
    public int swordDmg;
    public int pistolDmg;
    public int shotgunDmg;

    void Start()
    {
        daggerDmg = (int)Mathf.Pow(daggerLevel, 2);
        swordDmg = (int)Mathf.Pow(swordLevel, 2);
        pistolDmg = (int)Mathf.Pow(pistolLevel, 2);
        shotgunDmg = (int)Mathf.Pow(shotgunLevel, 2);
    }

    void SetWeaponDamage(int weaponDamage, int weaponLevel)
    {
        weaponDamage = (int)Mathf.Pow(weaponLevel, 2);
    }

    public int GetWeaponDamage()
    {
        if (GameData.WeaponEquipped == GameData.WeaponType.DAGGER)
        {
            return daggerDmg;
        }
        else if (GameData.WeaponEquipped == GameData.WeaponType.CUTLASS)
        {
            return swordDmg;
        }
        else if (GameData.WeaponEquipped == GameData.WeaponType.PISTOL)
        {
            return pistolDmg;
        }
        else if (GameData.WeaponEquipped == GameData.WeaponType.BLUNDERBUSS)
        {
            return shotgunDmg;
        }
        return -1;
    }

    public int GetNextUpgradeCost()
    {
        if (GameData.WeaponEquipped == GameData.WeaponType.DAGGER)
        {
            return (daggerLevel + 1) * 2;
        }
        else if (GameData.WeaponEquipped == GameData.WeaponType.CUTLASS)
        {
            return (swordLevel + 1) * 2;
        }
        else if (GameData.WeaponEquipped == GameData.WeaponType.PISTOL)
        {
            return (pistolLevel + 1) * 2;
        }
        else if (GameData.WeaponEquipped == GameData.WeaponType.BLUNDERBUSS)
        {
            return (shotgunLevel + 1) * 2;
        }
        return -1;
    }

    public void WeaponLevelUp(GameData.WeaponType weapon)
    {
        Debug.Log("Weapon upgraded");
        switch (weapon)
        {
            case (GameData.WeaponType.DAGGER):
                daggerLevel++;
                daggerDmg = (int)Mathf.Pow(daggerLevel, 2);
                break;
            case (GameData.WeaponType.CUTLASS):
                swordLevel++;
                swordDmg = (int)Mathf.Pow(swordLevel, 2);
                break;
            case (GameData.WeaponType.PISTOL):
                pistolLevel++;
                pistolDmg = (int)Mathf.Pow(pistolLevel, 2);
                break;
            case (GameData.WeaponType.BLUNDERBUSS):
                shotgunLevel++;
                shotgunDmg = (int)Mathf.Pow(shotgunLevel, 2);
                break;
        }
    }
}
