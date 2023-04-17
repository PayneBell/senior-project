using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    /*
        Public variables
    */

    public float moveSpeed;

    /*
        Package variables
    */

    Vector3 forward, right;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {

        // Generates forward and right vectors based on Camera's forward transform
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;

        // Sets movement direction in world space with GetAxisRaw
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));

        if (direction.magnitude > 0.1f)
        {

            // Generates orthographic movement vectors
            Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");
            Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");

            // Final movement vector, points in direction of player movement at any given tick
            Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

            transform.forward = heading;

            rb.velocity = heading * moveSpeed;
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }

    }

}
