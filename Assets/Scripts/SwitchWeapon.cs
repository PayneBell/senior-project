using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchWeapon : MonoBehaviour
{
    public TextMeshProUGUI weaponText;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameData.WeaponEquipped != GameData.WeaponType.DAGGER)
        {
            GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
            weaponText.text = "Current Weapon: Dagger";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GameData.WeaponEquipped != GameData.WeaponType.CUTLASS)
        {
            GameData.WeaponEquipped = GameData.WeaponType.CUTLASS;
            weaponText.text = "Current Weapon: Longsword";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GameData.WeaponEquipped != GameData.WeaponType.PISTOL)
        {
            GameData.WeaponEquipped = GameData.WeaponType.PISTOL;
            weaponText.text = "Current Weapon: Pistol";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GameData.WeaponEquipped != GameData.WeaponType.BLUNDERBUSS)
        {
            GameData.WeaponEquipped = GameData.WeaponType.BLUNDERBUSS;
            weaponText.text = "Current Weapon: Shotgun";
        }
    }
}
