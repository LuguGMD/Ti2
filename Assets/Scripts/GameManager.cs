using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int health = 100;
    public Slider healthBar;
    
    private int points;
    public TMP_Text pointsText;

    [SerializeField] private int combo = 0;
    public TMP_Text comboText;

    public void AddPoint()
    {
        points++;
        pointsText.SetText(points.ToString());

        combo++;
        UpdateComboText();
        Heal(10);
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthBar.value = health;

        // Breaks combo
        combo = 0;
        comboText.enabled = false;
    }

    void Heal(int heal)
    {
        if (health < 100 && combo >= 5)
        {
            health += heal;
            healthBar.value = health;
        }
    }

    void UpdateComboText()
    {
        comboText.SetText(combo + "  Combo");
        if (combo >= 5)
        {
            comboText.enabled = true; // Enables combo count if combo is greater than 5
        }
    }
}
