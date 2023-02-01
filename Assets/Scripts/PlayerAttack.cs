using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public float swingSpeed;
    public int swingDegrees;

    private bool attackUsable = true;
    private GameObject attackObject;
    private SpriteRenderer swingSprite;
    private BoxCollider swingCollider;
    private PlayerMove movementScript;

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
        if (Input.GetKeyDown(KeyCode.Space) && attackUsable)
        {
            movementScript.moveSpeed /= 2;
            swingSprite.enabled = true;

            attackObject.transform.Rotate(0, -swingDegrees / 2, 0);

            attackUsable = false;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < swingDegrees / swingSpeed; i++)
        {
            attackObject.transform.Rotate(Vector3.up, swingSpeed);

            yield return new WaitForSecondsRealtime(Time.deltaTime);
        }

        attackObject.transform.Rotate(0, -swingDegrees / 2, 0);

        attackUsable = true;

        movementScript.moveSpeed *= 2;
        swingSprite.enabled = false;
    }
}