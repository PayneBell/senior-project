using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    private GameObject attacker;

    void Start()
    {
        attacker = transform.parent.gameObject;
    }

    // Detects if enemy is hit by projectile
    void OnTriggerEnter(Collider other)
    {
        if (attacker.tag == "Enemy" && other.gameObject.tag == "Player")
        {
            EntityHealth healthScript = other.gameObject.GetComponent<EntityHealth>();

            healthScript.DamageEntity(1);
            healthScript.SetHealthText();

            if (healthScript.GetHealth() == 0)
                healthScript.KillEntity();
        }
        else if (attacker.tag == "Player" && other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            GameData.LiveEnemies.Remove(other.gameObject);
            StartCoroutine(RemoveFromList());
        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForFixedUpdate();

        GameData.LiveEnemies.RemoveAll(x => x == null);
    }
}
