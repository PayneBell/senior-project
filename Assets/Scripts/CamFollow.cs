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

    private Vector3 cameraPos;

    void Start() 
    {
        cameraPos = transform.position;    
    }

    void Update()
    {
        // Creates Vector3 goalPos for camera to move to and moves transform.position of Camera to goalPos
        if (target != null)
        {
            Vector3 goalPos = new Vector3(target.position.x + cameraPos.x, target.position.y + cameraPos.y, target.position.z + cameraPos.z);
            transform.position = Vector3.MoveTowards(transform.position, goalPos, smoothTime);
        }

    }
}
