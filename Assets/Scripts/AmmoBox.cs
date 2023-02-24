using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    GameObject player;
    SwitchWeapon weaponUIScript;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        weaponUIScript = player.GetComponent<SwitchWeapon>();

        transform.position = new Vector3(transform.position.x, transform.position.y + 0.25f, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
            GiveAmmo(4);
    }

    void GiveAmmo(int ammoAmt)
    {
        switch (GameData.WeaponEquipped)
        {
            case (GameData.WeaponType.PISTOL):
                player.GetComponent<UseWeapon>().reservePistolAmmo += ammoAmt;
                break;
            case (GameData.WeaponType.BLUNDERBUSS):
                player.GetComponent<UseWeapon>().reserveShotgunAmmo += ammoAmt / 2;
                break;
            default:
                if (Random.Range(0, 1) > 0)
                    player.GetComponent<UseWeapon>().reserveShotgunAmmo += ammoAmt / 2;
                else
                    player.GetComponent<UseWeapon>().reservePistolAmmo += ammoAmt;
                
                break;
        }
        weaponUIScript.UpdateUI(GameData.WeaponEquipped);
        Destroy(gameObject);
    }
}
