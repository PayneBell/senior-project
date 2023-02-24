using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchWeapon : MonoBehaviour
{
    public TextMeshProUGUI weaponText;

    GameObject player;
    UseWeapon weaponScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponScript = player.GetComponent<UseWeapon>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameData.WeaponEquipped != GameData.WeaponType.DAGGER)
        {
            GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
            weaponText.text = "Weapon: Dagger";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GameData.WeaponEquipped != GameData.WeaponType.CUTLASS)
        {
            GameData.WeaponEquipped = GameData.WeaponType.CUTLASS;
            weaponText.text = "Weapon: Longsword";
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) && GameData.WeaponEquipped != GameData.WeaponType.PISTOL)
        {
            GameData.WeaponEquipped = GameData.WeaponType.PISTOL;
            weaponText.text = string.Format("Weapon: Pistol ({0} / {1})", weaponScript.currentPistolAmmo, weaponScript.reservePistolAmmo);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) && GameData.WeaponEquipped != GameData.WeaponType.BLUNDERBUSS)
        {
            GameData.WeaponEquipped = GameData.WeaponType.BLUNDERBUSS;
            weaponText.text = string.Format("Weapon: Shotgun ({0} / {1})", weaponScript.currentShotgunAmmo, weaponScript.reserveShotgunAmmo);
        }
    }

    public void UpdateUI(GameData.WeaponType weaponUsed)
    {
        if (weaponUsed == GameData.WeaponType.PISTOL)
        {
            weaponText.text = string.Format("Weapon: Pistol ({0} / {1})", weaponScript.currentPistolAmmo, weaponScript.reservePistolAmmo);
        }
        else if (weaponUsed == GameData.WeaponType.BLUNDERBUSS)
        {
            weaponText.text = string.Format("Weapon: Shotgun ({0} / {1})", weaponScript.currentShotgunAmmo, weaponScript.reserveShotgunAmmo);
        }
    }
}
