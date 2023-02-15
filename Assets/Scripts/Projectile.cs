using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Detects if enemy is hit by projectile
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Enemy")
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
