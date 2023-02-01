using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    /*
        Public variables
    */

    public float spawnRadius;
    public int spawnLimit;
    public float spawnDelay;
    public GameObject enemyPrefab;

    /*
        Private variables
    */

    private int enemiesSpawned;

    void Start()
    {
        enemiesSpawned = 0;

        StartCoroutine(SpawnEnemy());
    }

    // Generates enemy spawn position as Vector3 by generating X and Z float positions within given spawnRadius
    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius);
        float spawnPosZ = Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius);

        return new Vector3(spawnPosX, 0f, spawnPosZ);
    }

    // Generates spawnLimit number of enemies within a Coroutine
    IEnumerator SpawnEnemy()
    {
        if (enemiesSpawned < spawnLimit)
        {
            Vector3 spawnPos = GenerateSpawnPosition();

            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemyObj.name = "Enemy" + enemiesSpawned;
            enemyObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.red);

            GameData.LiveEnemies.Add(enemyObj);
            enemiesSpawned++;

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(SpawnEnemy());
        }
    }

}
