using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class EntityHealth : MonoBehaviour
{
    public TextMeshProUGUI healthText;

    [SerializeField]
    private int healthPoints;

    public void DamageEntity(int damage)
    {
        healthPoints -= damage;
    }

    public int GetHealth()
    {
        return healthPoints;
    }

    public void SetHealthText()
    {
        healthText.text = "Health: " + GetHealth();
    }

    public void KillEntity()
    {
        GameData.WeaponEquipped = GameData.WeaponType.DAGGER;
        SceneManager.LoadScene(0);
    }
}
