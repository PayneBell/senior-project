using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackCooldown;

    private bool attackUsable = true;

    private bool attacking = false;

    private GameObject attackObject;

    void Start()
    {
        attackObject = GameObject.FindGameObjectWithTag("SwordAttack");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && attackUsable)
        {
            attackUsable = false;
            attacking = true;
            StartCoroutine(Attack());
        }

        if (attacking)
        {
            Swing();
        }

        //attackObject.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
    }

    void Swing()
    {
        attackObject.transform.Rotate(0f, attackObject.transform.rotation.y + 0.05f, 0f);

        if (attackObject.transform.rotation.y >= 30f)
            attacking = false;
    }

    IEnumerator Attack()
    {
        /*while (attackObject.transform.rotation.y < 30f && attackObject != null)
        {
            attackObject.transform.Rotate(0f, attackObject.transform.rotation.y + 0.05f, 0f);
        }*/

        yield return new WaitForSeconds(attackCooldown);
        attackUsable = true;
        attacking = false;
    }
}
