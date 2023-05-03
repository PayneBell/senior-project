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

    public int currentPistolAmmo;
    private int pistolMagSize;
    public int reservePistolAmmo;

    public int currentShotgunAmmo;
    private int shotgunMagSize;
    public int reserveShotgunAmmo;

    private bool attackAvailable = true;
    private float meleeCooldown;
    private float rangedCooldown;

    SwitchWeapon weaponUIScript;
    PlayerMove movementScript;
    Rigidbody rb;

    void Start()
    {
        pistolMagSize = currentPistolAmmo;
        shotgunMagSize = currentShotgunAmmo;

        rangedCooldown = endLag * 2f;
        meleeCooldown = endLag * 2f;

        weaponUIScript = GetComponent<SwitchWeapon>();
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
                    if (currentPistolAmmo > 0)
                    {
                        GameObject pistolBullet = Instantiate(bulletPrefab, transform.position + transform.forward, transform.rotation);
                        pistolBullet.GetComponent<Bullet>().shooter = gameObject;
                        pistolBullet.transform.forward = gameObject.transform.forward;

                        currentPistolAmmo--;
                        StartCoroutine(Cooldown(rangedCooldown));
                    }

                    break;

                case (GameData.WeaponType.BLUNDERBUSS):
                    if (currentShotgunAmmo > 0)
                    {
                        GameObject pellet1 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
                        pellet1.transform.forward = gameObject.transform.forward;
                        pellet1.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y - bulletSpread, 0f);
                        pellet1.GetComponent<Bullet>().shooter = gameObject;

                        GameObject pellet2 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
                        pellet1.transform.forward = gameObject.transform.forward;
                        pellet1.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y - (bulletSpread / 5), 0f);
                        pellet1.GetComponent<Bullet>().shooter = gameObject;

                        GameObject pellet3 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
                        pellet1.transform.forward = gameObject.transform.forward;
                        pellet1.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + (bulletSpread / 5), 0f);
                        pellet1.GetComponent<Bullet>().shooter = gameObject;

                        GameObject pellet4 = Instantiate(bulletPrefab, transform.position + transform.forward, Quaternion.identity);
                        pellet1.transform.forward = gameObject.transform.forward;
                        pellet1.transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + bulletSpread, 0f);
                        pellet1.GetComponent<Bullet>().shooter = gameObject;

                        currentShotgunAmmo--;
                        StartCoroutine(Cooldown(rangedCooldown));
                    }

                    break;

            }

            weaponUIScript.UpdateUI(GameData.WeaponTypeEquipped);
            StartCoroutine(FreezePlayer());
        }
        else if (Input.GetKeyDown(KeyCode.R) && ((reservePistolAmmo > 0 && currentPistolAmmo != pistolMagSize) || (reserveShotgunAmmo > 0 && currentShotgunAmmo != shotgunMagSize)))
        {
            ReloadWeapon(GameData.WeaponEquipped);
        }

    }

    void ReloadWeapon(GameData.WeaponType currentWeapon)
    {
        int bulletsRemaining;
        int bulletsToLoad;
        switch (currentWeapon)
        {
            case (GameData.WeaponType.PISTOL):
                bulletsRemaining = currentPistolAmmo;

                bulletsToLoad = pistolMagSize - bulletsRemaining;
                if (reservePistolAmmo - bulletsToLoad < 0)
                    bulletsToLoad = reservePistolAmmo;

                currentPistolAmmo += bulletsToLoad;
                reservePistolAmmo -= bulletsToLoad;
                break;
            case (GameData.WeaponType.BLUNDERBUSS):
                bulletsRemaining = currentShotgunAmmo;

                bulletsToLoad = shotgunMagSize - bulletsRemaining;
                if (reserveShotgunAmmo - bulletsToLoad < 0)
                    bulletsToLoad = reserveShotgunAmmo;

                currentShotgunAmmo += bulletsToLoad;
                reserveShotgunAmmo -= bulletsToLoad;
                break;
        }
        weaponUIScript.UpdateUIReload();
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
