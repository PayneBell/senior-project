using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUseWeapon : MonoBehaviour
{
    public float endLag;

    public GameData.WeaponType enemyWeapon;

    public GameObject daggerPrefab;
    public GameObject bulletPrefab;
    public float meleeCooldown;
    public float swingSpeed;
    public int swingDegrees;

    private EnemyFollow followScript;

    Coroutine attackRoutine;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        followScript = GetComponent<EnemyFollow>();
    }

    void Update()
    {
        if (followScript.following && attackRoutine == null)
        {
            switch (enemyWeapon)
            {
                case (GameData.WeaponType.DAGGER):
                    attackRoutine = StartCoroutine(MeleeAttack());
                    break;

                case (GameData.WeaponType.PISTOL):
                    attackRoutine = StartCoroutine(ShootPistol(bulletPrefab));
                    break;

            }
        }
    }

    IEnumerator MeleeAttack()
    {
        GameObject daggerSwingObj = Instantiate(daggerPrefab, transform);
        daggerSwingObj.transform.Rotate(0, -swingDegrees / 2, 0);

        // Rotates sword swing by swingSpeed degrees with each iteration
        for (int i = 0; i < swingDegrees / swingSpeed; i++)
        {
            daggerSwingObj.transform.Rotate(Vector3.up, swingSpeed);

            yield return new WaitForSeconds(0.01f);
        }

        Destroy(daggerSwingObj);

        if (followScript.following)
        {
            yield return new WaitForSeconds(meleeCooldown);
            attackRoutine = null;
        }

    }

    IEnumerator ShootPistol(GameObject prefab)
    {
        StartCoroutine(FreezeEnemy());

        GameObject pistolBullet = Instantiate(prefab, transform.position, transform.rotation);
        pistolBullet.GetComponent<Bullet>().shooter = gameObject;

        yield return new WaitForSeconds(meleeCooldown * 2);

        attackRoutine = null;
    }

    IEnumerator FreezeEnemy()
    {
        float baseSpeed = GetComponent<EnemyFollow>().enemySpeed;
        float speedAfterShot = baseSpeed / 2f;

        GetComponent<EnemyFollow>().enemySpeed = speedAfterShot;

        yield return new WaitForSeconds(endLag);

        GetComponent<EnemyFollow>().enemySpeed = baseSpeed;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
    }


}
