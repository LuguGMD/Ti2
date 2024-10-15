using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using UnityEditor.ShaderGraph.Internal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Health")]
    public int health = 100;
    public Slider healthBar;
    public bool invincible;
    [Header("Powerup")]
    public float powerupValue;
    public Slider powerupBar;
    public bool powerupActive = false;

    public float powerupDuration;

    [Header("HUD")]
    private int points;
    public TMP_Text pointsText;
    public TMP_Text victoryPanelPointsText;

    private int score;
    public TMP_Text scoreText;
    public TMP_Text victoryPanelScoreText;

    private int combo = 0;
    public TMP_Text comboText;

    public GameObject victoryPanel;
    public GameObject gameOverPanel;
    public GameObject pausePanel;

    public Action powerupStart;
    public Action powerupEnd;

    public Precision precisionValues;


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

    private void Start()
    {
        UpdateComboText();
        UpdateHealthBar();
        UpdatePowerupBar();
    }

    #region Values

    public void AddPoint()
    {
        points++;
        pointsText.SetText(points.ToString());

        combo++;
        UpdateComboText();
        Heal(10);
    }

    public void AddScore(float precision)
    {
        //Debug.Log(precision);

        float multiply = 1;
        List<ComboMultiply> combos = precisionValues.comboMultiplyList;
        
        for (int i = 0; i < combos.Count; i++)
        {
            if(combo >= combos[i].comboValue)
            {
                multiply = combos[i].comboMultiplier;
            }
        }

        if(powerupActive) multiply *= precisionValues.powerupMultiplier;

        int score = 0;

        if(precision > precisionValues.okCheck)
        {
            score = precisionValues.badScore;
        }
        else if(precision > precisionValues.greatCheck)
        {
            score = precisionValues.okScore;
        }
        else if (precision > precisionValues.perfectCheck)
        {
            score = precisionValues.greatScore;
        }
        else
        {
            score = precisionValues.perfectScore;
        }

        score = (int)(score * multiply);

        this.score += score;
        UpdateScoreBar(score);
        ChargePowerup(precision);
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

        ChargePowerup(-1);
    }

    void Heal(int heal)
    {
        if (health < 100 && combo >= 5)
        {
            health += heal;
            UpdateHealthBar();
        }
    }

    void ChargePowerup(float precision)
    {
        if (!powerupActive)
        {
            if(precision < 0)
            {
                //powerupValue -= precisionValues.badCheck;
                powerupValue = 0;
            }
            else
            {
                powerupValue += precisionValues.badCheck - precision;
            }

            powerupValue = Mathf.Clamp(powerupValue, 0, 100); 

            if(powerupValue >= 100)
            {
                EnablePowerup();
            }

            UpdatePowerupBar();
        }
    }

    public void EnablePowerup()
    {
        powerupActive = true;
        StartCoroutine(UsePowerup());
        powerupStart?.Invoke();
    }

    public void DisablePowerup()
    {
        powerupActive = false;
        powerupEnd?.Invoke();
    }

    IEnumerator UsePowerup()
    {
        while (powerupActive)
        {
            yield return new WaitForSeconds(powerupDuration/100);

            if (Time.timeScale != 0f)
            {
                powerupValue -= 1;

                powerupValue = Mathf.Clamp(powerupValue, 0, 100);

                if (powerupValue <= 0)
                {
                    DisablePowerup();
                }

                UpdatePowerupBar();
            }
        }
    }

    #endregion

    #region HUD

    void UpdateComboText()
    {
        comboText.SetText(combo + "  Combo");
        if (combo >= 5)
        {
            comboText.enabled = true; // Enables combo count if combo is greater than 5
        }
    }

    void UpdatePowerupBar()
    {
        powerupBar.value = powerupValue;
    }

    public void UpdateHealthBar()
    {
        healthBar.value = health;
    }

    public void UpdateScoreBar(float value)
    {
        scoreText.SetText(score.ToString());
        //Value is the difference added (use it to vfx)
    }

    public void Victory()
    {
        victoryPanel.SetActive(true);
        victoryPanelPointsText.SetText(points.ToString());
        victoryPanelScoreText.SetText(score.ToString());
        Time.timeScale = 0f;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    #endregion

    #region Game States

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

    #endregion
}

[System.Serializable]
public class Precision
{
    private const float maxVal = 3.3f;

    [Header("Check")]
    [Range(maxVal, maxVal)]
    public float badCheck = maxVal;
    [Range(0f, maxVal)]
    public float okCheck;
    [Range(0f, maxVal)]
    public float greatCheck;
    [Range(0f, maxVal)]
    public float perfectCheck;
    [Header("Scores")]
    public int badScore;
    public int okScore;
    public int greatScore;
    public int perfectScore;
    [Header("Multipliers")]
    public float powerupMultiplier;
    public List<ComboMultiply> comboMultiplyList;
}
[System.Serializable]
public class ComboMultiply
{
    public int comboValue;
    public float comboMultiplier;
}

