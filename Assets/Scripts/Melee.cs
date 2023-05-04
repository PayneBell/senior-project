using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Melee : MonoBehaviour
{
    private GameObject attacker;

    private int meleeDamage;

    WinGame winGameScript;
    PointSystem pointSystem;
    InventoryManager playerInventory;

    void Start()
    {
        attacker = transform.parent.gameObject;

        winGameScript = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<WinGame>();
        pointSystem = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<PointSystem>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();

        meleeDamage = playerInventory.GetWeaponDamage();

    }

    // Detects if enemy is hit by projectile
    void OnTriggerEnter(Collider other)
    {
        if (attacker.tag == "Enemy" && other.gameObject.tag == "Player")
        {
            EntityHealth healthScript = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<EntityHealth>();

            healthScript.DamageEntity(1);

            if (healthScript.GetHealth() == 0)
                healthScript.KillEntity();
        }
        else if (attacker.tag == "Player" && other.gameObject.tag == "Enemy")
        {
            Debug.Log("enemy hit");

            other.gameObject.GetComponentInChildren<EnemyHealth>().DealDamage(meleeDamage);

            if (other.gameObject.GetComponentInChildren<EnemyHealth>().GetHealth() <= 0)
            {
                Destroy(other.gameObject);
                GameData.LiveEnemies.Remove(other.gameObject);
                StartCoroutine(RemoveFromList());
            }

        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForFixedUpdate();

        GameData.LiveEnemies.RemoveAll(x => x == null);
    }
}
