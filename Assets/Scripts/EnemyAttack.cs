using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAttack : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);

            foreach (GameObject enemy in GameData.LiveEnemies)
            {
                if (enemy != null)
                    enemy.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }

            StartCoroutine(WaitAfterDeath());
        }
    }

    IEnumerator WaitAfterDeath()
    {
        yield return new WaitForSeconds(1f);

        GameData.EquippedMelee = GameData.WeaponType.NONE;
        GameData.EquippedRanged = GameData.WeaponType.NONE;

        SceneManager.LoadScene(0);
    }
}
