using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 1f;

    private Vector3 velocity = Vector3.zero;

    void Update()
    {
        Vector3 goalPos = new Vector3(target.position.x - 7f, target.position.y + 6.5f, target.position.z - 7f);
        transform.position = Vector3.MoveTowards(transform.position, goalPos, smoothTime);
    }
}
