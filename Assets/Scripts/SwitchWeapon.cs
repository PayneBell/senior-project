using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SwitchWeapon : MonoBehaviour
{
    public GameObject ammoTextBox;

    public TextMeshProUGUI weaponText;
    public TextMeshProUGUI levelText;

    public RectTransform meleeSlot;
    public RectTransform rangedSlot;

    GameObject player;
    UseWeapon weaponScript;
    InventoryManager playerInventory;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponScript = player.GetComponent<UseWeapon>();

        playerInventory = player.GetComponent<InventoryManager>();

        meleeSlot.localScale = new Vector3(6f, 6f, 1f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && GameData.WeaponTypeEquipped == GameData.SlotType.RANGED)
        {
            GameData.WeaponTypeEquipped = GameData.SlotType.MELEE;
            GameData.WeaponEquipped = GameData.EquippedMelee;

            rangedSlot.localScale = new Vector3(5f, 5f, 1f);
            meleeSlot.localScale = new Vector3(6f, 6f, 1f);

            UpdateUI(GameData.WeaponTypeEquipped);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GameData.WeaponTypeEquipped == GameData.SlotType.MELEE)
        {
            GameData.WeaponTypeEquipped = GameData.SlotType.RANGED;
            GameData.WeaponEquipped = GameData.EquippedRanged;

            meleeSlot.localScale = new Vector3(5f, 5f, 1f);
            rangedSlot.localScale = new Vector3(6f, 6f, 1f);

            UpdateUI(GameData.WeaponTypeEquipped);
        }
    }

    public void UpdateUI(GameData.SlotType newSlot)
    {
        if (newSlot == GameData.SlotType.MELEE)
        {
            ammoTextBox.SetActive(false);

            if (GameData.EquippedMelee == GameData.WeaponType.DAGGER)
            {
                weaponText.text = "DAGGER";
                levelText.text = string.Format("Lvl {0}", playerInventory.daggerLevel);
            }
            else if (GameData.EquippedMelee == GameData.WeaponType.CUTLASS)
            {
                weaponText.text = "CUTLASS";
                levelText.text = string.Format("Lvl {0}", playerInventory.swordLevel);
            }

        }
        else if (newSlot == GameData.SlotType.RANGED)
        {

            if (GameData.EquippedRanged == GameData.WeaponType.PISTOL)
            {
                weaponText.text = "PISTOL";
                levelText.text = string.Format("Lvl {0}", playerInventory.pistolLevel);

                ammoTextBox.SetActive(true);
                ammoTextBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0} / {1}", weaponScript.currentPistolAmmo, weaponScript.reservePistolAmmo);
            }
            else if (GameData.EquippedRanged == GameData.WeaponType.BLUNDERBUSS)
            {
                weaponText.text = "SHOTGUN";
                levelText.text = string.Format("Lvl {0}", playerInventory.shotgunLevel);

                ammoTextBox.SetActive(true);
                ammoTextBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0} / {1}", weaponScript.currentShotgunAmmo, weaponScript.reserveShotgunAmmo);
            }
        }
    }

    public void UpdateUIReload()
    {
        if (GameData.EquippedRanged == GameData.WeaponType.PISTOL)
            ammoTextBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0} / {1}", weaponScript.currentPistolAmmo, weaponScript.reservePistolAmmo);
        else if (GameData.EquippedRanged == GameData.WeaponType.BLUNDERBUSS)
            ammoTextBox.GetComponentInChildren<TextMeshProUGUI>().text = string.Format("{0} / {1}", weaponScript.currentShotgunAmmo, weaponScript.reserveShotgunAmmo);
    }
}
