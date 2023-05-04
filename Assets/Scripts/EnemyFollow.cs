using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float enemySight;
    public float enemySpeed;
    public float enemyFOV;

    public bool following = false;

    Rigidbody rb;
    GameObject player;

    Ray sightRay;
    RaycastHit sightRayHit;

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
                if (!following)
                    StartCoroutine(StartFollowing());

                rb.velocity = transform.forward * enemySpeed;
                transform.LookAt(player.transform);
            }
            else
            {
                StopFollowing();
            }

            sightRay = new Ray(new Vector3(transform.position.x, transform.position.y + 1f, transform.position.z), transform.forward);

            if (Physics.Raycast(sightRay, out sightRayHit, enemySight))
            {
                if (sightRayHit.collider == null && following)
                {
                    StopFollowing();
                }
            }
        }

        if (following)
        {
            rb.velocity = transform.forward * enemySpeed;
            transform.LookAt(player.transform);
        }

    }

    void StopFollowing()
    {
        following = false;

        GetComponent<EnemyUseWeapon>().enabled = false;

        rb.velocity = Vector3.zero;
    }

    IEnumerator StartFollowing()
    {
        following = true;

        yield return new WaitForSeconds(Random.Range(0f, 1f));

        GetComponent<EnemyUseWeapon>().enabled = true;
    }
}
