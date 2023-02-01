using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    /*
        Public variables
    */

    public Transform target;
    public float smoothTime;

    /*
        Private variables
    */

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        // Creates Vector3 goalPos for camera to move to and moves transform.position of Camera to goalPos
        if (target != null)
        {
            Vector3 goalPos = new Vector3(target.position.x - 7f, target.position.y + 6.5f, target.position.z - 7f);
            transform.position = Vector3.MoveTowards(transform.position, goalPos, smoothTime);
        }

    }
}
