using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenChest : MonoBehaviour
{
    public GameObject openChestPrefab;
    public GameData.WeaponType itemContained;

    public float interactionRadius;

    public RectTransform rangedSlot;
    public RectTransform meleeSlot;

    [SerializeField]
    private bool openable;
    private MouseHighlight mouseRayScript;

    private GameObject player;

    Camera mainCam;

    public GameObject switchPrompt;

    public GameObject daggerIcon;
    public GameObject cutlassIcon;
    public GameObject pistolIcon;
    public GameObject shotgunIcon;

    public GameObject weaponLabel;
    public GameObject weaponLevel;

    public bool opening = false;

    void Start()
    {
        mainCam = Camera.main;
        mouseRayScript = mainCam.GetComponent<MouseHighlight>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        openable = Vector3.Distance(player.transform.position, transform.position) < interactionRadius;

        if (Physics.Raycast(mouseRayScript.cameraRay, out mouseRayScript.cameraRayHit, Mathf.Infinity, 1 << 8))
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && openable && mouseRayScript.cameraRayHit.collider.tag == "Chest")
            {

                switch (itemContained)
                {
                    case (GameData.WeaponType.PISTOL):
                        if (GameData.EquippedRanged == GameData.WeaponType.NONE)
                        {
                            GameData.EquippedRanged = GameData.WeaponType.PISTOL;
                            pistolIcon.SetActive(true);

                            player.GetComponent<SwitchWeapon>().SwitchSlot(GameData.SlotType.RANGED, GameData.WeaponType.PISTOL, rangedSlot, meleeSlot);

                            Instantiate(openChestPrefab, transform.position, transform.rotation);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            Time.timeScale = 0f;
                            switchPrompt.SetActive(true);
                            switchPrompt.GetComponent<SwitchPrompt>().chest = gameObject;
                        }

                        break;
                    case (GameData.WeaponType.BLUNDERBUSS):
                        if (GameData.EquippedRanged == GameData.WeaponType.NONE)
                        {
                            GameData.EquippedRanged = GameData.WeaponType.BLUNDERBUSS;
                            shotgunIcon.SetActive(true);

                            player.GetComponent<SwitchWeapon>().SwitchSlot(GameData.SlotType.RANGED, GameData.WeaponType.BLUNDERBUSS, rangedSlot, meleeSlot);

                            Instantiate(openChestPrefab, transform.position, transform.rotation);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            Time.timeScale = 0f;
                            switchPrompt.SetActive(true);
                            switchPrompt.GetComponent<SwitchPrompt>().chest = gameObject;
                        }
                        break;
                    case (GameData.WeaponType.DAGGER):
                        if (GameData.EquippedMelee == GameData.WeaponType.NONE)
                        {
                            GameData.EquippedMelee = GameData.WeaponType.DAGGER;
                            daggerIcon.SetActive(true);

                            player.GetComponent<SwitchWeapon>().SwitchSlot(GameData.SlotType.MELEE, GameData.WeaponType.DAGGER, rangedSlot, meleeSlot);

                            Instantiate(openChestPrefab, transform.position, transform.rotation);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            Time.timeScale = 0f;
                            switchPrompt.SetActive(true);
                            switchPrompt.GetComponent<SwitchPrompt>().chest = gameObject;
                        }
                        break;
                    case (GameData.WeaponType.CUTLASS):
                        if (GameData.EquippedMelee == GameData.WeaponType.NONE)
                        {
                            GameData.EquippedMelee = GameData.WeaponType.CUTLASS;
                            cutlassIcon.SetActive(true);

                            player.GetComponent<SwitchWeapon>().SwitchSlot(GameData.SlotType.MELEE, GameData.WeaponType.BLUNDERBUSS, rangedSlot, meleeSlot);

                            Instantiate(openChestPrefab, transform.position, transform.rotation);
                            gameObject.SetActive(false);
                        }
                        else
                        {
                            Time.timeScale = 0f;
                            switchPrompt.SetActive(true);
                            switchPrompt.GetComponent<SwitchPrompt>().chest = gameObject;
                        }
                        break;
                }
            }
        }
    }

    void Yes(GameData.SlotType slot)
    {
        switch (slot)
        {
            case (GameData.SlotType.MELEE):
                daggerIcon.SetActive(false);

                GameData.EquippedMelee = GameData.WeaponType.CUTLASS;
                cutlassIcon.SetActive(true);

                player.GetComponent<SwitchWeapon>().SwitchSlot(GameData.SlotType.MELEE, GameData.WeaponType.CUTLASS, rangedSlot, meleeSlot);
                break;
            case (GameData.SlotType.RANGED):
                pistolIcon.SetActive(false);

                GameData.EquippedRanged = GameData.WeaponType.BLUNDERBUSS;
                shotgunIcon.SetActive(true);

                player.GetComponent<SwitchWeapon>().SwitchSlot(GameData.SlotType.RANGED, GameData.WeaponType.BLUNDERBUSS, rangedSlot, meleeSlot);
                break;
        }

        Time.timeScale = 1f;
        switchPrompt.SetActive(false);

        player.GetComponent<SwitchWeapon>().UpdateUI(slot);
        gameObject.SetActive(false);

        Instantiate(openChestPrefab, transform.position, transform.rotation);
        gameObject.SetActive(false);
    }

    void No()
    {
        opening = false;
        gameObject.SetActive(true);

        Time.timeScale = 1f;
        switchPrompt.SetActive(false);
    }

}
