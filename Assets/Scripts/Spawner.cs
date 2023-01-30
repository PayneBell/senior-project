using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public float spawnRadius;
    public int spawnLimit;
    public float spawnDelay;

    public GameObject enemyPrefab;

    private int enemiesSpawned;

    void Start()
    {
        enemiesSpawned = 0;

        StartCoroutine(SpawnEnemy());
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(transform.position.x - spawnRadius, transform.position.x + spawnRadius);
        float spawnPosZ = Random.Range(transform.position.z - spawnRadius, transform.position.z + spawnRadius);

        return new Vector3(spawnPosX, 1f, spawnPosZ);
    }

    IEnumerator SpawnEnemy()
    {
        if (enemiesSpawned < spawnLimit)
        {
            Vector3 spawnPos = GenerateSpawnPosition();

            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
            enemiesSpawned++;

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(SpawnEnemy());
        }
    }

}
