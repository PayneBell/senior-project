using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime;
    public float bulletSpeed;

    public GameObject ammoBoxPrefab;

    [HideInInspector]
    public GameObject shooter;

    GameObject mouseHighlight;
    WinGame winGameScript;

    void Start()
    {
        StartCoroutine(DestroyBullet());

        mouseHighlight = GameObject.FindGameObjectWithTag("MouseHighlight");
        winGameScript = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<WinGame>();

        transform.position = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);
        //transform.forward = shooter.transform.forward;
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (shooter != null)
        {
            if (shooter.tag == "Player" && other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
                GameData.LiveEnemies.Remove(other.gameObject);
                winGameScript.enemiesRemainingText.text = "Enemies Remaining: " + GameData.LiveEnemies.Count;
                StartCoroutine(RemoveFromList());

                bool dropAmmo = Random.Range(0, 100) < GameData.ammoDropChance;
                if (dropAmmo)
                    Instantiate(ammoBoxPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
            }

            else if (shooter.tag == "Enemy" && other.gameObject.tag == "Player")
            {
                EntityHealth healthScript = other.gameObject.GetComponent<EntityHealth>();

                healthScript.DamageEntity(1);
                healthScript.SetHealthText();

                if (healthScript.GetHealth() == 0)
                    healthScript.KillEntity();
            }

            else if (other.gameObject.tag == "Level")
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletLifetime);

        Destroy(gameObject);
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForFixedUpdate();

        GameData.LiveEnemies.RemoveAll(x => x == null);
        Destroy(gameObject);
    }

    IEnumerator KillPlayerAndReload(GameObject playerObj)
    {
        StopCoroutine(DestroyBullet());
        Destroy(playerObj);

        yield return new WaitForSeconds(1f);

        GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
        SceneManager.LoadScene(0);
    }
}
