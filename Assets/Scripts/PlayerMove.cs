using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed;

    private Rigidbody rb;

    private float moveAxisX;
    private float moveAxisZ;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        moveAxisX = Input.GetAxis("Horizontal") * moveSpeed;
        moveAxisZ = Input.GetAxis("Vertical") * moveSpeed;

        rb.velocity = new Vector3(moveAxisX, rb.velocity.y, moveAxisZ);

        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal") + " | " + "Vertical: " + Input.GetAxis("Vertical"));
    }

    void Update()
    { }
}
