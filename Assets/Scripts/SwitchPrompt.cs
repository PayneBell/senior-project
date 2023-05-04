using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SwitchPrompt : MonoBehaviour
{
    [HideInInspector]
    public GameObject chest;

    public TextMeshProUGUI promptText;

    void Start()
    {
        if (chest.GetComponent<OpenChest>().itemContained == GameData.WeaponType.DAGGER || chest.GetComponent<OpenChest>().itemContained == GameData.WeaponType.CUTLASS)
            promptText.text = string.Format("You found a {0}, but you already have a melee weapon equipped, would you like to switch? This will permanently remove the old weapon from your inventory.", chest.GetComponent<OpenChest>().itemContained.ToString().ToLower());
        
        if (chest.GetComponent<OpenChest>().itemContained == GameData.WeaponType.PISTOL || chest.GetComponent<OpenChest>().itemContained == GameData.WeaponType.BLUNDERBUSS)
            promptText.text = string.Format("You found a {0}, but you already have a ranged weapon equipped, would you like to switch? This will permanently remove the old weapon from your inventory.", chest.GetComponent<OpenChest>().itemContained.ToString().ToLower());

        
    }

    public void Yes()
    {
        chest.SetActive(true);

        if (chest.GetComponent<OpenChest>().itemContained == GameData.WeaponType.CUTLASS)
            chest.SendMessage("Yes", GameData.SlotType.MELEE);
        else if (chest.GetComponent<OpenChest>().itemContained == GameData.WeaponType.BLUNDERBUSS)
            chest.SendMessage("Yes", GameData.SlotType.RANGED);

        chest.SetActive(false);
    }

    public void No()
    {
        chest.SetActive(true);
        chest.SendMessage("No");
    }
}
