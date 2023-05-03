using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCam : MonoBehaviour
{
    public Transform pivot;
    public Transform ship;

    void Start()
    {
        pivot.transform.position = new Vector3(ship.position.x, ship.position.y + 10f, ship.position.z);
    }

    void Update()
    {
        transform.RotateAround(pivot.position, Vector3.up, 10 * Time.deltaTime);
    }
}
