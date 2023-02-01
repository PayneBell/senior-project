using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && GetComponent<SpriteRenderer>().enabled)
        {
            Destroy(other.gameObject);
        }
    }
}
