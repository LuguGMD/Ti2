using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    
    public SaveSystem saveSystem;

    public GameObject[] levelButtons;
    public TMP_Text[] levelCoinsText;
    public TMP_Text[] levelScoreText;
    public TMP_Text currentCoinsText;

    public SkinManager skinManager;
    public PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerData = saveSystem.LoadPlayerData();

        if (playerData == null)
        {
            // If there is no save, create a default save
            PlayerData defaultSave = new PlayerData(2, 5);
            defaultSave.SetLevelInfo(0, 0, true, 0);
            defaultSave.LoadAchivements();
            saveSystem.SavePlayerData(defaultSave);
        }
        else
        {
            // Loads levels info in the level selection screen 
            for (int i = 0; i < playerData.levels.Length; i++)
            {
                levelCoinsText[i].SetText(playerData.levels[i].coins.ToString());
                levelScoreText[i].SetText(playerData.levels[i].score.ToString());

                // Check which levels the player has unlocked
                if (playerData.levels[i].unlocked)
                {
                    levelButtons[i].GetComponent<Button>().interactable = true;
                }
            }

            // Loads achivements progress
            List<Achievement> listedAchivements = AchievementSystem.instance.achievements;

            for (int i = 0; i < playerData.achivements.Length; i++)
            {
                listedAchivements[i].value = playerData.achivements[i].value;
                listedAchivements[i].unlocked = playerData.achivements[i].unlocked;
            }

            // Loads how many coins the player currently has
            currentCoinsText.SetText(playerData.currentCoins.ToString());

            // Loads unlocked skins
            for (int i = 0; i < skinManager.skinsList.Count; i++)
            {
                skinManager.skinsList[i].unlocked = playerData.skins[i];
            }

            skinManager.SetSkin(playerData.equippedSkinIndex); // Changes equipped skin
        }
    }

    public void UpdateCurrentCoinsText(float value)
    {
        currentCoinsText.SetText(value.ToString());
    }
}
