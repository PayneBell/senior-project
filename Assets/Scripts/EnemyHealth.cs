using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public Slider healthSlider;

    PointSystem pointSystem;

    bool pointsGiven = false;

    public GameObject ammoBoxPrefab;
    public GameObject rumPrefab;

    void Start()
    {
        pointSystem = GameObject.FindGameObjectWithTag("GameMgr").GetComponent<PointSystem>();
    }

    public int GetHealth()
    {
        return (int)healthSlider.value;
    }

    public void SetMaxHealth()
    {
        int health;

        if (GameData.CurrentWave != 1)
            health = (int)Mathf.Pow(GameData.BaseRangedEnemyHealth * GameData.CurrentWave, 2f);
        else
            health = GameData.BaseRangedEnemyHealth * GameData.CurrentWave;

        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void DealDamage(int dmg)
    {
        SetHealth(GetHealth() - dmg);

        Debug.Log(dmg + " damage dealt, enemy health goes from " + (GetHealth() + dmg) + " to " + GetHealth());

        if (GetHealth() <= 0 && !pointsGiven)
        {
            GiveEnemyDrops();
        }
    }

    public int GetMaxHealth()
    {
        return (int)healthSlider.maxValue;
    }


    void GiveEnemyDrops()
    {
        GameData.CurrentPoints++;
        pointSystem.UpdatePoints();

        Transform enemy = transform.parent.transform.parent;

        enemy.GetComponent<SpawnerTracker>().spawner.GetComponent<Spawner>().liveEnemies--;

        int drop = Random.Range(0, 100);

        if (drop >= 0 && drop <= GameData.ammoDropChance)
        {
            GameObject ammo = Instantiate(ammoBoxPrefab, enemy.position, enemy.rotation);
        }

        else if (drop >= GameData.ammoDropChance && drop <= (GameData.rumDropChance + GameData.ammoDropChance))
        {
            GameObject rum = Instantiate(rumPrefab, new Vector3(enemy.position.x, enemy.position.y + 2, enemy.position.z), enemy.rotation);
        }

        pointsGiven = true;

    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
