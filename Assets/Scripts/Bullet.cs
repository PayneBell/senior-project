using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime;
    public float bulletSpeed;

    private int bulletDamage;

    public GameObject ammoBoxPrefab;

    [HideInInspector]
    public GameObject shooter;

    GameObject mouseHighlight;
    WinGame winGameScript;
    PointSystem pointSystem;
    InventoryManager playerInventory;

    void Start()
    {
        StartCoroutine(DestroyBullet());

        mouseHighlight = GameObject.FindGameObjectWithTag("MouseHighlight");
        winGameScript = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<WinGame>();
        pointSystem = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<PointSystem>();
        playerInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<InventoryManager>();

        bulletDamage = playerInventory.GetWeaponDamage();

        transform.position = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);

        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void OnTriggerEnter(Collider other)
    {
        if (shooter != null)
        {
            if (shooter.tag == "Player" && other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyHealth>().DealDamage(bulletDamage);

                if (other.gameObject.GetComponent<EnemyHealth>().GetHealth() <= 0)
                {
                    GameData.CurrentPoints++;
                    pointSystem.UpdatePoints();

                    Destroy(other.gameObject);
                    GameData.LiveEnemies.Remove(other.gameObject);
                    winGameScript.enemiesRemainingText.text = "Enemies Remaining: " + GameData.LiveEnemies.Count;
                    StartCoroutine(RemoveFromList());
                }

                bool dropAmmo = Random.Range(0, 100) < GameData.ammoDropChance;
                if (dropAmmo)
                    Instantiate(ammoBoxPrefab, other.gameObject.transform.position, other.gameObject.transform.rotation);
            }

            else if (shooter.tag == "Enemy" && other.gameObject.tag == "Player")
            {
                EntityHealth healthScript = GameObject.FindGameObjectWithTag("UI").GetComponentInChildren<EntityHealth>();

                healthScript.DamageEntity(1);

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

    IEnumerator RestartOnDeath(GameObject playerObj)
    {
        StopCoroutine(DestroyBullet());
        Destroy(playerObj);

        yield return new WaitForSeconds(1f);

        GameData.EquippedMelee = GameData.WeaponType.DAGGER;
        GameData.EquippedRanged = GameData.WeaponType.PISTOL;

        SceneManager.LoadScene(0);
    }
}
