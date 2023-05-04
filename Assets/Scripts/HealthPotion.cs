using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameData.HealthPots++;

            player.GetComponent<SwitchWeapon>().UpdateUIRum();

            Destroy(gameObject);
        }
    }
}
