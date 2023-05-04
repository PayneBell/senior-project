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
    public TextMeshProUGUI waveCounterText;

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
            SwitchSlot(GameData.SlotType.MELEE, GameData.EquippedMelee, rangedSlot, meleeSlot);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && GameData.WeaponTypeEquipped == GameData.SlotType.MELEE)
        {
            SwitchSlot(GameData.SlotType.RANGED, GameData.EquippedRanged, rangedSlot, meleeSlot);
        }
    }

    public void SwitchSlot(GameData.SlotType slot, GameData.WeaponType weapon, RectTransform rangedSlot, RectTransform meleeSlot)
    {
        GameData.WeaponTypeEquipped = slot;
        GameData.WeaponEquipped = weapon;

        if (slot == GameData.SlotType.MELEE)
        {
            rangedSlot.localScale = new Vector3(5f, 5f, 1f);
            meleeSlot.localScale = new Vector3(6f, 6f, 1f);
        }
        else
        {
            meleeSlot.localScale = new Vector3(5f, 5f, 1f);
            rangedSlot.localScale = new Vector3(6f, 6f, 1f);
        }

        UpdateUI(slot);
    }

    public void UpdateUI(GameData.SlotType newSlot)
    {
        if (!weaponText.gameObject.activeSelf)
        {
            weaponText.gameObject.SetActive(true);
            if (!levelText.gameObject.activeSelf)
                levelText.gameObject.SetActive(true);
        }

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

    public void UpdateUIWave()
    {
        waveCounterText.text = GameData.CurrentWave.ToString();
    }
}
