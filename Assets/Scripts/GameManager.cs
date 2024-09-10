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

    private int combo = 0;
    public TMP_Text comboText;

    public GameObject victoryPanel;
    public GameObject gameOverPanel;

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

        // Game Over trigger
        if (health <= 0)
        {
            GameOver();
        }
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

    public void Victory()
    {
        victoryPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }
}
