using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
        Public variables
    */

    public float minSpawnRadius;
    public float maxSpawnRadius;
    public int spawnLimit;
    public int liveEnemies;
    public int enemiesSpawned;
    public float spawnDelay;
    public GameObject enemyPrefab;
    public List<GameObject> enemies = new List<GameObject>();

    /*
        Private variables
    */




    /*
        Package variables
    */

    GameObject player;

    public bool spawning;

    void Start()
    {
        GameData.LiveSpawners.Add(gameObject);

        liveEnemies = spawnLimit;
        enemiesSpawned = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < maxSpawnRadius * 2 && !spawning)
        {
            spawning = true;
            StartCoroutine(SpawnEnemy());
        }
    }

    // Generates enemy spawn position within a circle around level center with minimum and maximum spawn radius
    Vector3 GenerateSpawnPosition()
    {
        Vector3 spawnCenter = transform.position;
        Vector3 convertedPos;

        // Generates a spawn position until generated position falls outside of minimum spawn radius
        do
        {
            Vector2 spawnPos = (Random.insideUnitCircle * maxSpawnRadius) + new Vector2(spawnCenter.x, spawnCenter.z);
            convertedPos = new Vector3(spawnPos.x, 0f, spawnPos.y);
        } while (Vector3.Distance(spawnCenter, convertedPos) < minSpawnRadius);


        return convertedPos;
    }

    // Generates spawnLimit number of enemies
    IEnumerator SpawnEnemy()
    {
        if (enemiesSpawned < spawnLimit)
        {
            Vector3 spawnPos = GenerateSpawnPosition();

            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemyObj.GetComponent<SpawnerTracker>().spawner = gameObject;

            enemyObj.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);

            if (enemyPrefab.tag == "Melee")
                enemyObj.GetComponent<EnemyHealth>().SetHealth(GameData.BaseMeleeEnemyHealth * GameData.CurrentWave);
            else if (enemyPrefab.tag == "Ranged")
                enemyObj.GetComponent<EnemyHealth>().SetHealth(GameData.BaseRangedEnemyHealth * GameData.CurrentWave);

            enemyObj.name = "Enemy" + enemiesSpawned;
            enemyObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.red);

            GameData.LiveEnemies.Add(enemyObj);
            enemiesSpawned++;
            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(SpawnEnemy());
        }
    }

}
