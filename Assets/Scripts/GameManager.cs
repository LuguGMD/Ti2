using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int health = 100;
    public Slider healthBar;
    public bool invincible;
    
    private int points;
    public TMP_Text pointsText;
    public TMP_Text victoryPanelPointsText;

    private int combo = 0;
    public TMP_Text comboText;

    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;


    private void Awake()
    {
        Time.timeScale = 1f;
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

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
        if (!invincible)
        {
            health -= damage;
            UpdateHealthBar();
        }

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
            UpdateHealthBar();
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

    public void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    public void Victory()
    {
        victoryPanel.SetActive(true);
        victoryPanelPointsText.SetText(points.ToString());
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void SceneChange(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Quit in Unity Editor
#endif

        Application.Quit(); // Quit in build
    }
}
