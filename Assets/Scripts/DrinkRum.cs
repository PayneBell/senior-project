using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrinkRum : MonoBehaviour
{

    bool drinkable = true;

    EntityHealth playerHealthScript;

    void Start()
    {
        playerHealthScript = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<EntityHealth>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && GameData.HealthPots > 0 && drinkable && playerHealthScript.GetHealth() < playerHealthScript.GetMaxHealth())
        {
            drinkable = false;
            StartCoroutine(Cooldown());

            if (playerHealthScript.GetMaxHealth() - playerHealthScript.GetHealth() < 5)
                playerHealthScript.AddHealth(playerHealthScript.GetMaxHealth() - playerHealthScript.GetHealth());
            else
                playerHealthScript.AddHealth(5);
            
            playerHealthScript.SetHealth(playerHealthScript.GetHealth());
            
            GameData.HealthPots--;

            GetComponent<SwitchWeapon>().UpdateUIRum();
        }
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(3f);

        drinkable = true;
    }
}
