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
        achievements[ind].value += value;
    }

    public void UnlockAchievement(int ind)
    {
        achievements[ind].unlocked = true;
        popUp?.Invoke(achievements[ind]);
    }

    public void CheckUnlocked()
    {
        for (int i = 0; i < achievements.Count; i++) 
        {
            if (achievements[i].unlocked)
            {
                if (achievements[i].value >= achievements[i].requirement)
                {
                    UnlockAchievement(i);
                }
            }
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
