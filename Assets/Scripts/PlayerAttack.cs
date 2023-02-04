using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    /*
        Public variables
    */

    public GameObject weaponPrefab;

    public float attackCooldown;
    public float swingSpeed;
    public int swingDegrees;

    /*
        Private variables
    */

    private bool attackUsable = true;
    private SpriteRenderer swingSprite;
    private BoxCollider swingCollider;
    private PlayerMove movementScript;

    /*
        Package variables
    */

    Rigidbody rb;

    void Start()
    {
        swingCollider = GetComponentInChildren<BoxCollider>();
        movementScript = GetComponent<PlayerMove>();

        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Listens for user attack input
        if (Input.GetKeyDown(KeyCode.Space) && attackUsable)
        {
            GameObject weaponObj = Instantiate(weaponPrefab, transform);
            movementScript.enabled = false;
            rb.velocity = Vector3.zero;

            weaponObj.transform.Rotate(0, -swingDegrees / 2, 0);

            attackUsable = false;
            StartCoroutine(Attack(weaponObj));
        }
    }

    // Controls attack cooldown
    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(attackCooldown);

        attackUsable = true;
    }

    // Attack coroutine
    IEnumerator Attack(GameObject weaponObj)
    {
        // Rotates sword swing by swingSpeed degrees with each iteration
        for (int i = 0; i < swingDegrees / 2; i++)
        {
            weaponObj.transform.Rotate(Vector3.up, 2f);

            yield return new WaitForSeconds(Time.fixedDeltaTime / swingSpeed);
        }

        // Returns sword swing to default position to prepare for next swing
        weaponObj.transform.forward = -transform.forward;

        StartCoroutine(Cooldown());

        Destroy(weaponObj);
        movementScript.enabled = true;
    }
}