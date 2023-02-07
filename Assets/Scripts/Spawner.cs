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

    // Generates enemy spawn position within a circle around level center with minimum and maximum spawn radius
    Vector3 GenerateSpawnPosition()
    {
        Vector3 worldCenter = GameObject.FindGameObjectWithTag("Level").transform.position;
        Vector3 convertedPos;

        // Generates a spawn position until generated position falls outside of minimum spawn radius
        do
        {
            Vector2 spawnPos = Random.insideUnitCircle * maxSpawnRadius;
            convertedPos = new Vector3(spawnPos.x, 0f, spawnPos.y);
        } while (Vector3.Distance(worldCenter, convertedPos) < minSpawnRadius);


        return convertedPos;
    }

    // Generates spawnLimit number of enemies
    IEnumerator SpawnEnemy()
    {
        if (enemiesSpawned < spawnLimit)
        {
            Vector3 spawnPos = GenerateSpawnPosition();

            GameObject enemyObj = Instantiate(enemyPrefab, spawnPos, Quaternion.Euler(0f, Random.Range(0, 360), 0f));
            enemyObj.name = "Enemy" + enemiesSpawned;
            enemyObj.GetComponentInChildren<MeshRenderer>().material.SetColor("_Color", Color.red);

            GameData.LiveEnemies.Add(enemyObj);
            enemiesSpawned++;

            yield return new WaitForSeconds(spawnDelay);
            StartCoroutine(SpawnEnemy());
        }
    }

}
