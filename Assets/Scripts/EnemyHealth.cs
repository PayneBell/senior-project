using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    private int maxHealth;
    public int enemyHealth;

    MeshRenderer enemyMesh;

    void Start()
    {
        maxHealth = enemyHealth;

        enemyMesh = GetComponentInChildren<MeshRenderer>();
    }

    public void DamageEntity(int damage)
    {
        enemyHealth -= damage;
    }

    public int GetHealth()
    {
        return enemyHealth;
    }

    public void DealDamage(int dmg)
    {
        enemyHealth -= dmg;
    }

    public void KillEnemy()
    {
        Destroy(gameObject);
    }
}
