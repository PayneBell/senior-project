using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    SwitchWeapon uiScript;

    public int spawnersDisabled = 0;

    public int spawnersInLevel;

    void Start()
    {
        uiScript = GameObject.FindGameObjectWithTag("Player").GetComponent<SwitchWeapon>();

        StartCoroutine(CheckSpawners());
    }

    IEnumerator CheckSpawners()
    {
        foreach (GameObject spawner in GameData.LiveSpawners)
        {
            if (spawner && spawner.GetComponent<Spawner>().liveEnemies == 0)
            {
                MeshRenderer spawnRenderer = spawner.GetComponentInChildren<MeshRenderer>();

                if (spawnRenderer.material.color != Color.green)
                {
                    spawnRenderer.material.color = Color.green;

                    DisableSpawner(spawner);

                    spawnersDisabled++;
                }
            }
        }

        // Move to next wave if all spawners disabled
        if (spawnersDisabled == spawnersInLevel)
        {
            GameData.CurrentWave++;

            uiScript.UpdateUIWave();


            foreach (GameObject spawner in GameData.LiveSpawners)
            {
                MeshRenderer spawnRenderer = spawner.GetComponentInChildren<MeshRenderer>();

                EnableSpawner(spawner);

                spawnRenderer.material.color = Color.red;
            }

            spawnersDisabled = 0;
        }
        else
        {
            yield return new WaitForSeconds(1);

            StartCoroutine(CheckSpawners());
        }
    }

    void DisableSpawner(GameObject spawner)
    {
        spawner.GetComponent<Spawner>().enabled = false;
    }

    void EnableSpawner(GameObject spawner)
    {
        spawner.GetComponent<Spawner>().enabled = true;

        spawner.GetComponent<Spawner>().liveEnemies = 10;
        spawner.GetComponent<Spawner>().enemiesSpawned = 0;

        StartCoroutine(GracePeriod(spawner));
        
    }

    IEnumerator GracePeriod(GameObject spawner)
    {
        yield return new WaitForSeconds(5f);

        spawner.GetComponent<Spawner>().spawning = false;
    }
}
