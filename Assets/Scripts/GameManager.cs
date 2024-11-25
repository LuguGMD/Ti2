using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
//using UnityEditor.ShaderGraph.Internal;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Animator playerAnim;

    [Header("Health")]
    public int health = 100;
    public Slider healthBar;
    public bool invincible;
    [Header("Powerup")]
    public float powerupValue;
    public Slider powerupBar;
    public bool powerupActive = false;
    [Range(1f,5f)]
    public float powerupMult;

    public float powerupDuration;

    [Header("HUD")]
    private int coins; // Number of coins the player has gotten in the current level
    public TMP_Text coinsText;
    public TMP_Text victoryPanelCoinsText;

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

    [Header("Control variable")]
    public bool gamePaused = false;

    [Header("Save Configuration")]
    public SaveSystem saveSystem;
    public int currentLevelId;

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
        gamePaused = false;

        UpdateComboText();
        UpdateHealthBar();
        UpdatePowerupBar();
    }

    #region Values

    public void AddPoint()
    {
        coins++;
        coinsText.SetText(coins.ToString());

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
            StartCoroutine(Camera.main.GetComponent<CameraShake>().Shake()); // Activates Camera Shake
        }

        // Breaks combo
        combo = 0;
        comboText.enabled = false;

        playerAnim.SetTrigger("Damaged");

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
                powerupValue += (precisionValues.badCheck - precision)*powerupMult;
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

    #endregion

    #region Game States

    public void Victory()
    {
        victoryPanel.SetActive(true);
        victoryPanelCoinsText.SetText(coins.ToString());
        victoryPanelScoreText.SetText(score.ToString());
        Time.timeScale = 0f;

        SetPlayerData();
    }

    public void GameOver()
    {
        gamePaused = true; // Used to deactivate player input

        AudioController.instance.ChangeBGMusic(0);
        
        playerAnim.SetTrigger("Died");
        playerAnim.SetLayerWeight(1, 1f);
        gameOverPanel.SetActive(true);

        // Stops background and enemies motion
        GameObject background = GameObject.Find("Player");
        background.GetComponent<Move>().XSpeed = 0;
    }

    public void SceneChange(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads active scene
    }

    public void PauseGame()
    {
        gamePaused = true;

        pausePanel.SetActive(true);
        Time.timeScale = 0f;

        AudioController.instance.PauseMusic();
    }

    public void UnpauseGame()
    {
        gamePaused = false;

        pausePanel.SetActive(false);
        Time.timeScale = 1f;

        AudioController.instance.PlayMusic();
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Quit in Unity Editor
#endif

        Application.Quit(); // Quit in build
    }

    // Prepares player data to save info accoding to the current level
    public void SetPlayerData()
    {
        PlayerData playerData = saveSystem.LoadPlayerData();

        if (playerData != null)
        {
            Level currentLevel = playerData.levels[currentLevelId];

            if (coins > currentLevel.coins)
            {
                playerData.currentCoins += coins - currentLevel.coins;
                currentLevel.coins = coins;
            }

            if (score > currentLevel.score)
            {
                currentLevel.score = score;
            }

            if (currentLevelId + 1 != playerData.levels.Length)
            {
                playerData.levels[currentLevelId + 1].unlocked = true; // Unlocks the next level
            }

            saveSystem.SavePlayerData(playerData);
        }
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

