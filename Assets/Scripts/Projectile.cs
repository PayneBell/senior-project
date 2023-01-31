using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && GetComponent<SpriteRenderer>().enabled)
        {
            Destroy(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && GetComponent<SpriteRenderer>().enabled)
        {
            Destroy(other.gameObject);
        }
    }
}
