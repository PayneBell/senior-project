using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UseWeapon : MonoBehaviour
{
    public float endLag;
    public GameObject bulletPrefab;
    public GameObject daggerSwingPrefab;
    public GameObject cutlassSwingPrefab;

    public float swingSpeed;
    public int swingDegrees;

    // Bullet spread (for blunderbuss) in degrees
    public int bulletSpread;

    private bool attackAvailable = true;
    private float meleeCooldown;
    private float rangedCooldown;

    PlayerMove movementScript;
    Rigidbody rb;

    void Start()
    {
        rangedCooldown = endLag * 2f;
        meleeCooldown = endLag;

        movementScript = GetComponent<PlayerMove>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && attackAvailable)
        {
            switch (GameData.WeaponEquipped)
            {
                case (GameData.WeaponType.DAGGER):
                    GameObject daggerSwingObj = Instantiate(daggerSwingPrefab, transform);
                    daggerSwingObj.transform.Rotate(0, -swingDegrees / 2, 0);

                    StartCoroutine(MeleeAttack(daggerSwingObj));

                    break;

                case (GameData.WeaponType.CUTLASS):
                    GameObject cutlassSwingObj = Instantiate(cutlassSwingPrefab, transform);
                    cutlassSwingObj.transform.Rotate(0, -swingDegrees / 2, 0);

                    StartCoroutine(MeleeAttack(cutlassSwingObj));

                    break;

                case (GameData.WeaponType.PISTOL):
                    GameObject pistolBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
                    pistolBullet.GetComponent<Bullet>().shooter = gameObject;

                    StartCoroutine(Cooldown(rangedCooldown));
                    break;

                case (GameData.WeaponType.BLUNDERBUSS):
                    GameObject pellet1 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - bulletSpread, 0f));
                    pellet1.GetComponent<Bullet>().shooter = gameObject;

                    GameObject pellet2 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - (bulletSpread / 5), 0f));
                    pellet2.GetComponent<Bullet>().shooter = gameObject;

                    GameObject pellet3 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.Euler(0f, transform.rotation.eulerAngles.y + (bulletSpread / 5), 0f));
                    pellet3.GetComponent<Bullet>().shooter = gameObject;

                    GameObject pellet4 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.Euler(0f, transform.rotation.eulerAngles.y + bulletSpread, 0f));
                    pellet4.GetComponent<Bullet>().shooter = gameObject;

                    StartCoroutine(Cooldown(rangedCooldown));

                    break;

            }

            StartCoroutine(FreezePlayer());
        }

    }

    IEnumerator MeleeAttack(GameObject weaponObj)
    {
        // Rotates sword swing by swingSpeed degrees with each iteration
        for (int i = 0; i < swingDegrees / swingSpeed; i++)
        {
            weaponObj.transform.Rotate(Vector3.up, swingSpeed);

            yield return new WaitForSeconds(0.01f);
        }

        StartCoroutine(Cooldown(meleeCooldown));
        Destroy(weaponObj);

    }

    IEnumerator FreezePlayer()
    {
        movementScript.enabled = false;
        rb.velocity = Vector3.zero;

        yield return new WaitForSeconds(endLag);

        movementScript.enabled = true;
    }

    IEnumerator Cooldown(float cooldownTime)
    {
        attackAvailable = false;

        yield return new WaitForSeconds(cooldownTime);

        attackAvailable = true;
    }
}
