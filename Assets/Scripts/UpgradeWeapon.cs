using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeWeapon : MonoBehaviour
{
    public float interactionRadius;

    public GameObject insufficientText;

    private MouseHighlight mouseRayScript;

    private GameObject player;

    private bool interactable;

    Camera mainCam;
    InventoryManager playerInventory;
    PointSystem pointSystem;

    SwitchWeapon weaponSwitch;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = Camera.main;
        mouseRayScript = mainCam.GetComponent<MouseHighlight>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<InventoryManager>();
        pointSystem = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<PointSystem>();
        weaponSwitch = player.GetComponent<SwitchWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        interactable = Vector3.Distance(player.transform.position, transform.position) < interactionRadius;

        if (Physics.Raycast(mouseRayScript.cameraRay, out mouseRayScript.cameraRayHit, Mathf.Infinity, 1 << 8))
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) && interactable && mouseRayScript.cameraRayHit.collider.tag == "Anvil" && GameData.WeaponEquipped != GameData.WeaponType.NONE)
            {
                if (GameData.CurrentPoints >= playerInventory.GetNextUpgradeCost())
                {
                    GameData.CurrentPoints -= playerInventory.GetNextUpgradeCost();
                    pointSystem.UpdatePoints();

                    playerInventory.WeaponLevelUp(GameData.WeaponEquipped);

                    weaponSwitch.UpdateUI(GameData.WeaponTypeEquipped);
                }
                else
                {
                    insufficientText.SetActive(true);
                    insufficientText.GetComponent<TextMeshProUGUI>().text = string.Format("Insufficient points for upgrade points required: {0}", playerInventory.GetNextUpgradeCost());
                    StartCoroutine(RemoveInsufficientText(insufficientText));
                    //Debug.Log(string.Format("Insufficient points for upgrade, points required: {0}", playerInventory.GetNextUpgradeCost()));
                }

            }
        }
    }

    IEnumerator RemoveInsufficientText(GameObject text)
    {
        yield return new WaitForSeconds(3f);

        text.SetActive(false);


    }
}
