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


    Ray cameraRay;
    RaycastHit cameraRayHit;
    BoxCollider swingCollider;
    PlayerMove movementScript;
    Rigidbody rb;

    void Start()
    {
        rangedCooldown = endLag * 2f;
        meleeCooldown = endLag;

        swingCollider = GetComponentInChildren<BoxCollider>();
        movementScript = GetComponent<PlayerMove>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);

        int layerMask = 1 << 6;

        if (Input.GetKeyDown(KeyCode.Mouse0) && attackAvailable)
        {
            if (Physics.Raycast(cameraRay, out cameraRayHit, Mathf.Infinity, layerMask))
            {
                Vector3 targetPosition = new Vector3(cameraRayHit.point.x - 1.5f, cameraRayHit.point.y, cameraRayHit.point.z - 1.5f);

                transform.LookAt(targetPosition);
            }

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
                    Instantiate(bulletPrefab, transform.position, transform.rotation);

                    StartCoroutine(Cooldown(rangedCooldown));
                    break;

                case (GameData.WeaponType.BLUNDERBUSS):
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - bulletSpread, 0f));
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, transform.rotation.eulerAngles.y - (bulletSpread/5), 0f));
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, transform.rotation.eulerAngles.y + (bulletSpread/5), 0f));
                    Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0f, transform.rotation.eulerAngles.y + bulletSpread, 0f));

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

            yield return new WaitForSeconds(Time.fixedDeltaTime / swingSpeed);
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
