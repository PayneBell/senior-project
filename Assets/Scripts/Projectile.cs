using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    // Executes if swing begins outside enemy collider
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && GetComponentInChildren<SpriteRenderer>().enabled)
        {
            Destroy(other.gameObject);
            GameData.LiveEnemies.Remove(other.gameObject);
            StartCoroutine(RemoveFromList());
        }
    }


    // Executes if swing begins inside enemy collider
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && GetComponentInChildren<SpriteRenderer>().enabled)
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
