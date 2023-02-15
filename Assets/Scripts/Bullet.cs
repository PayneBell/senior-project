using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bullet : MonoBehaviour
{
    public float bulletLifetime;
    public float bulletSpeed;

    [HideInInspector]
    public GameObject shooter;

    GameObject mouseHighlight;

    void Start()
    {
        StartCoroutine(DestroyBullet());

        mouseHighlight = GameObject.FindGameObjectWithTag("MouseHighlight");

        transform.position = new Vector3(transform.position.x, transform.position.y + 1.25f, transform.position.z);
        //transform.forward = shooter.transform.forward;
        GetComponent<Rigidbody>().velocity = transform.forward * bulletSpeed;
    }

    void OnCollisionEnter(Collision other)
    {
        if (shooter != null)
        {
            if (shooter.tag == "Player" && other.gameObject.tag == "Enemy")
            {
                Destroy(other.gameObject);
                GameData.LiveEnemies.Remove(other.gameObject);
                StartCoroutine(RemoveFromList());
            }

            else if (shooter.tag == "Enemy" && other.gameObject.tag == "Player")
            {
                GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
                SceneManager.LoadScene(0);
            }

            else if (other.gameObject.tag == "Level")
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bulletLifetime);

        Destroy(gameObject);
    }

    IEnumerator RemoveFromList()
    {
        yield return new WaitForFixedUpdate();

        GameData.LiveEnemies.RemoveAll(x => x == null);
        Destroy(gameObject);
    }

    IEnumerator KillPlayerAndReload(GameObject playerObj)
    {
        StopCoroutine(DestroyBullet());
        Destroy(playerObj);

        yield return new WaitForSeconds(1f);

        GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
        SceneManager.LoadScene(0);
    }
}
