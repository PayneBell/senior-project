using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int swingDegrees;

    public bool attacking = false;

    private bool attackUsable = true;

    private GameObject attackObject;

    private SpriteRenderer swingSprite;

    private BoxCollider swingCollider;

    void Start()
    {
        attackObject = GameObject.FindGameObjectWithTag("SwordAttack");

        swingSprite = GetComponentInChildren<SpriteRenderer>();

        swingCollider = GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && attackUsable)
        {
            swingSprite.enabled = true;
            //swingCollider.enabled = true;

            attackObject.transform.eulerAngles = new Vector3(0, 315, 0);

            attackUsable = false;
            attacking = true;
            StartCoroutine(Attack());
        }
    }

    IEnumerator Attack()
    {
        for (int i = 0; i < swingDegrees / 2; i++)
        {
            attackObject.transform.Rotate(new Vector3(0f, 2f, 0f));

            yield return new WaitForSeconds(1f / 100f);
        }

        attackObject.transform.eulerAngles = new Vector3(0, 315, 0);

        attacking = false;
        attackUsable = true;

        swingSprite.enabled = false;
        //swingCollider.enabled = false;
    }
}
