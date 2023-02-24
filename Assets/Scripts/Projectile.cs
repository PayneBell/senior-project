using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Projectile : MonoBehaviour
{
    private GameObject attacker;

    WinGame winGameScript;

    void Start()
    {
        attacker = transform.parent.gameObject;

        winGameScript = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<WinGame>();
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
            winGameScript.enemiesRemainingText.text = "Enemies Remaining: " + GameData.LiveEnemies.Count;
            StartCoroutine(RemoveFromList());
        }
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForFixedUpdate();

        GameData.LiveEnemies.RemoveAll(x => x == null);
    }
}
