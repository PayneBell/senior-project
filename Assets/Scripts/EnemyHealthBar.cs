using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform cam;

    void Start()
    {
        cam = Camera.main.transform;

        GetComponent<Canvas>().worldCamera = Camera.main;
    }

    void LateUpdate()
    {
        Vector3 wantedPos = Camera.main.ScreenToWorldPoint(cam.position);

        transform.position = wantedPos;
    }
}
