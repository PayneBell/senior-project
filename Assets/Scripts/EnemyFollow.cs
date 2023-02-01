using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float sightRadius;
    public float enemySpeed;

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
            if (Vector3.Distance(player.transform.position, transform.position) > sightRadius)
            {
                rb.velocity = Vector3.zero;
            }
            else
            {
                transform.LookAt(player.transform);
                rb.velocity = transform.forward * enemySpeed;
            }
        }

    }
}
