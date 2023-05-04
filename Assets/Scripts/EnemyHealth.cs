using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth;

    private int enemyHealth;

    PointSystem pointSystem;

    bool pointsGiven = false;

    public GameObject ammoBoxPrefab;

    void Start()
    {
        pointSystem = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<PointSystem>();

        maxHealth = enemyHealth;
    }

    public void DamageEntity(int damage)
    {
        enemyHealth -= damage;
    }

    public int GetHealth()
    {
        return enemyHealth;
    }

    public void SetHealth(int health)
    {
        enemyHealth = health;
    }

    public void DealDamage(int dmg)
    {
        enemyHealth -= dmg;


        if (enemyHealth <= 0 && !pointsGiven)
        {
            GiveEnemyDrops();
        }
    }

    void GiveEnemyDrops()
    {
        GameData.CurrentPoints++;
        pointSystem.UpdatePoints();

        GetComponent<SpawnerTracker>().spawner.GetComponent<Spawner>().liveEnemies--;

        bool dropAmmo = Random.Range(0, 100) < GameData.ammoDropChance;
        if (dropAmmo)
            Instantiate(ammoBoxPrefab, transform.position, gameObject.transform.rotation);

        pointsGiven = true;

    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
