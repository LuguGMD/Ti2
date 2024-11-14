using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public int coins;
    public int score;
    public bool unlocked;

    public Level(int coins, int score, bool unlocked)
    {
        this.coins = coins;
        this.score = score;
        this.unlocked = unlocked;
    }
}
