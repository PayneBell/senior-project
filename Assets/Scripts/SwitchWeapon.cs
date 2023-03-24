using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchWeapon : MonoBehaviour
{
    public TextMeshProUGUI weaponText;

    GameObject player;
    UseWeapon weaponScript;
    InventoryManager playerInventory;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponScript = player.GetComponent<UseWeapon>();

        playerInventory = player.GetComponent<InventoryManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameData.WeaponEquipped != GameData.WeaponType.DAGGER)
        {
            GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
            weaponText.text = string.Format("Weapon: Dagger (Lvl {0})", playerInventory.daggerLevel);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GameData.WeaponEquipped != GameData.WeaponType.CUTLASS)
        {
            GameData.WeaponEquipped = GameData.WeaponType.CUTLASS;
            weaponText.text = string.Format("Weapon: Cutlass (Lvl {0})", playerInventory.swordLevel);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GameData.WeaponEquipped != GameData.WeaponType.PISTOL)
        {
            GameData.WeaponEquipped = GameData.WeaponType.PISTOL;
            weaponText.text = string.Format("Weapon: Pistol (Lvl {0}) ({1} / {2})", playerInventory.pistolLevel, weaponScript.currentPistolAmmo, weaponScript.reservePistolAmmo);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GameData.WeaponEquipped != GameData.WeaponType.BLUNDERBUSS)
        {
            GameData.WeaponEquipped = GameData.WeaponType.BLUNDERBUSS;
            weaponText.text = string.Format("Weapon: Shotgun (Lvl {0}) ({0} / {1})", playerInventory.shotgunLevel, weaponScript.currentShotgunAmmo, weaponScript.reserveShotgunAmmo);
        }
    }

    public void UpdateUI(GameData.WeaponType weaponUsed)
    {
        if (weaponUsed == GameData.WeaponType.DAGGER)
        {
            weaponText.text = string.Format("Weapon: Dagger (Lvl {0})", playerInventory.daggerLevel);
        }
        else if (weaponUsed == GameData.WeaponType.CUTLASS)
        {
            weaponText.text = string.Format("Weapon: Cutlass (Lvl {0})", playerInventory.swordLevel);
        }
        else if (weaponUsed == GameData.WeaponType.PISTOL)
        {
            weaponText.text = string.Format("Weapon: Pistol (Lvl {0}) ({1} / {2})", playerInventory.pistolLevel, weaponScript.currentPistolAmmo, weaponScript.reservePistolAmmo);
        }
        else if (weaponUsed == GameData.WeaponType.BLUNDERBUSS)
        {
            weaponText.text = string.Format("Weapon: Shotgun (Lvl {0}) ({1} / {2})", playerInventory.shotgunLevel, weaponScript.currentShotgunAmmo, weaponScript.reserveShotgunAmmo);
        }
    }
}
