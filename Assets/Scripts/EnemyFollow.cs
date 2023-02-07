using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float enemySight;
    public float enemySpeed;
    public float enemyFOV;

    Rigidbody rb;

    GameObject player;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            Vector3 playerToEnemy = player.transform.position - transform.position;
            float playerDist = Vector3.Distance(player.transform.position, transform.position);
            if (Vector3.Angle(transform.forward, playerToEnemy) < enemyFOV && playerDist < enemySight)
            {
                transform.LookAt(player.transform);
                rb.velocity = transform.forward * enemySpeed;
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }

    }
}
