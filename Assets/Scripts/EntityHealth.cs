using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EntityHealth : MonoBehaviour
{
    public Slider healthSlider;

    public TextMeshProUGUI maxHealthText;
    public TextMeshProUGUI currentHealthText;

    [SerializeField]
    private int healthPoints;

    public void DamageEntity(int damage)
    {
        healthPoints -= damage;
        SetHealth(healthPoints);
    }

    public int GetHealth()
    {
        return (int)healthSlider.value;
    }

    public void AddHealth(int health)
    {
        healthPoints += health;
        SetHealth(health);
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;

        maxHealthText.text = health.ToString();
    }


    public void SetHealth(int health)
    {
        healthSlider.value = health;

        currentHealthText.text = health.ToString();
    }

    public void KillEntity()
    {
        GameData.EquippedMelee = GameData.WeaponType.NONE;
        GameData.EquippedRanged = GameData.WeaponType.NONE;

        SceneManager.LoadScene(0);
    }
}
