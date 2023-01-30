using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackPrefab;
    public float attackCooldown;

    private bool attackUsable = true;

    private GameObject attackObject;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && attackUsable)
        {
            StartCoroutine(Attack());
        }

        if (attackObject != null)
        {
            attackObject.transform.RotateAround(transform.position, Vector3.up, 20 * Time.deltaTime);
        }
    }

    IEnumerator Attack()
    {
        attackUsable = false;
        attackObject = Instantiate(attackPrefab, transform.position, Quaternion.identity);

        yield return new WaitForSeconds(attackCooldown);
        Destroy(attackObject);
        attackUsable = true;
    }
}
