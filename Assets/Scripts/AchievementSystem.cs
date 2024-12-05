using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum ACHIEVE
{
    test1,
    test2,
    test3,
    test4,
    test5
}
     

public class AchievementSystem : MonoBehaviour
{
    public static AchievementSystem instance;

    public List<Achievement> achievements;

    public Action<Achievement> popUp;

    public PlayerData playerData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateAchievement(int ind, float value)
    {
        if (!achievements[ind].unlocked && achievements[ind].value < achievements[ind].requirement)
        {
            achievements[ind].value += value;
        }
    }

    public void UnlockAchievement(int ind)
    {
        achievements[ind].unlocked = true;
        popUp?.Invoke(achievements[ind]);
        Debug.Log("Achivement " + achievements[ind].name + "unlocked!");
    }

    public void SaveAchievement(int ind)
    {
        if (playerData != null)
        {
            Debug.Log("Save");
            playerData.achivements[ind].value = achievements[ind].value;
            playerData.achivements[ind].unlocked = achievements[ind].unlocked;
        }
    }

    public void CheckUnlocked()
    {
        if (GameManager.instance.saveSystem != null)
        {
            playerData = GameManager.instance.saveSystem.LoadPlayerData();
        }
        else if (SaveManager.instance.saveSystem != null)
        {
            playerData = SaveManager.instance.saveSystem.LoadPlayerData();
        }

        Debug.Log(playerData.achivements[7].value);
        

        for (int i = 0; i < achievements.Count; i++) 
        {
            if (!achievements[i].unlocked)
            {
                if (achievements[i].value >= achievements[i].requirement)
                {
                    UnlockAchievement(i);
                }
            }

            SaveAchievement(i);
        }


        if (GameManager.instance.saveSystem != null)
        {
            GameManager.instance.saveSystem.SavePlayerData(playerData);
        }
        else if (SaveManager.instance.saveSystem != null)
        {
            SaveManager.instance.saveSystem.SavePlayerData(playerData);
        }
    }

}

[System.Serializable]
public class Achievement
{
    [Header("Info")]
    public string name;
    public string description;
    public Sprite icon;
    public ACHIEVE id;
    [Header("Stats")]
    public float value;
    public float requirement;
    public bool unlocked;
}
