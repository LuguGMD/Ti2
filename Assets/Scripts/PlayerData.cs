using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Level[] levels;
    public int currentCoins;

    public PlayerData(int levelsLength)
    {
        levels = new Level[levelsLength];
        currentCoins = 0;
    }
}
