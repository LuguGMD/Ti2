using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Level[] levels;
    public int currentCoins;

    public PlayerData(int numberOfLevels)
    {
        levels = new Level[numberOfLevels];
        currentCoins = 0;
    }

    public void SetLevelInfo(int coins, int score, bool unlocked, int levelId)
    {
        Level level = new Level(coins, score, unlocked);
        levels[levelId] = level;
    }
}
