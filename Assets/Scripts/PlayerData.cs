using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public LevelSaveData[] levels;
    public AchievementSaveData[] achivements;
    public RankingList rankings;
    public bool[] skins;
    public int currentCoins;
    public int equippedSkinIndex;

    public PlayerData(int numberOfLevels, int numberOfSkins)
    {
        levels = new LevelSaveData[numberOfLevels];
        skins = new bool[numberOfSkins];
        skins[0] = true;
        rankings = new RankingList(numberOfLevels);
        currentCoins = 0;
    }

    public void SetLevelInfo(int coins, int score, bool unlocked, int levelId)
    {
        LevelSaveData level = new LevelSaveData(coins, score, unlocked);
        levels[levelId] = level;
    }

    public void LoadAchivements()
    {
        List<Achievement> listedAchivements = AchievementSystem.instance.achievements;
        
        achivements = new AchievementSaveData[listedAchivements.Count];
        for (int i = 0; i < listedAchivements.Count; i++)
        {
            achivements[i] = new AchievementSaveData();
            achivements[i].name = listedAchivements[i].name;
            achivements[i].value = listedAchivements[i].value;
            achivements[i].unlocked = listedAchivements[i].unlocked;
        }
    }
}

[System.Serializable]
public class LevelSaveData
{
    public int coins;
    public int score;
    public bool unlocked;

    public LevelSaveData(int coins, int score, bool unlocked)
    {
        this.coins = coins;
        this.score = score;
        this.unlocked = unlocked;
    }
}

[System.Serializable]
public class AchievementSaveData
{
    public string name;
    public float value;
    public bool unlocked;

    public AchievementSaveData(string name = "", float value = 0, bool unlocked = false)
    {
        this.name = name;
        this.value = value;
        this.unlocked = unlocked;
    }
}

[System.Serializable]
public class Ranking
{
    public List<Player> ranking;
}

[System.Serializable]
public class RankingList
{
    public List<Ranking> rankingList;

    public RankingList(int numberOfRankings)
    {
        rankingList = new List<Ranking>();

        for (int i = 0; i < numberOfRankings; i++)
        {
            rankingList.Add(new Ranking());
        }
    }


    public int Count(int index)
    {
        return rankingList[index].ranking.Count;
    }

    public Player GetPlayerRanking(int rankingIndex, int playerIndex)
    {
        return rankingList[rankingIndex].ranking[playerIndex];
    }

    public void SetPlayerRanking(int rankingIndex, List<Player> ranking)
    {
        rankingList[rankingIndex].ranking = ranking;
    }
}
