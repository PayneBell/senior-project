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
        moveAxisX = Input.GetAxisRaw("Horizontal") * moveSpeed;
        moveAxisZ = Input.GetAxisRaw("Vertical") * moveSpeed;

        rb.velocity = new Vector3(moveAxisX, rb.velocity.y, moveAxisZ);
    }

    void Update()
    { }
}
