using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*
        Public variables
    */

    public float attackCooldown;
    public float swingSpeed;
    public int swingDegrees;

    /*
        Private variables
    */

    private bool attackUsable = true;
    private GameObject attackObject;
    private SpriteRenderer swingSprite;
    private BoxCollider swingCollider;
    private PlayerMove movementScript;

    /*
        Package variables
    */

    Rigidbody rb;

    void Start()
    {
        attackObject = GameObject.FindGameObjectWithTag("SwordAttack");
        swingSprite = GetComponentInChildren<SpriteRenderer>();
        swingCollider = GetComponentInChildren<BoxCollider>();
        movementScript = GetComponent<PlayerMove>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Listens for user attack input
        if (Input.GetKeyDown(KeyCode.Space) && attackUsable)
        {
            movementScript.moveSpeed /= 2;
            swingSprite.enabled = true;

            attackObject.transform.Rotate(0, -swingDegrees / 2, 0);

            attackUsable = false;
            StartCoroutine(Cooldown());
            StartCoroutine(Attack());
        }
    }

    // Controls attack cooldown
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);

        attackUsable = true;
    }

    // Attack coroutine
    IEnumerator Attack()
    {
        // Rotates sword swing by swingSpeed degrees with each iteration
        for (int i = 0; i < swingDegrees / swingSpeed; i++)
        {
            attackObject.transform.Rotate(Vector3.up, swingSpeed);

            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

        // Returns sword swing to default position to prepare for next swing
        attackObject.transform.Rotate(0, -swingDegrees / 2, 0);

        movementScript.moveSpeed *= 2;
        swingSprite.enabled = false;
    }
}