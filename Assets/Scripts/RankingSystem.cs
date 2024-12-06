using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class RankingSystem : MonoBehaviour
{
    [Header("Ranking List")]
    public List<Player> ranking;

    [Header("Input")]
    public TMP_InputField nameInput;

    private void Awake()
    {     
        SortRankList();
    }

    public void AddRank(string name, int score)
    {
        Player p = new Player(name, score);
        ranking.Add(p);
    }

    public void UpdateRank()
    {
        string name = nameInput.text;

        if (name != "")
        {
            bool exist = false;
            int ind = 0;

            for (int i = 0; i < ranking.Count; i++)
            {
                if (ranking[i].name == name)
                {
                    exist = true;
                    ind = i;
                }
            }

            if (exist)
            {
                ranking[ind].score = Mathf.Max(GameManager.instance.score, ranking[ind].score);
            }
            else
            {
                AddRank(name, GameManager.instance.score);
            }

            SortRankList();

            // Saves ranking
            PlayerData playerData = GameManager.instance.saveSystem.LoadPlayerData();
            playerData.rankings.SetPlayerRanking(GameManager.instance.currentLevelId, ranking);
            GameManager.instance.saveSystem.SavePlayerData(playerData);
        }
    }

    public void SortRankList()
    {
        bool passed = true;
        int last = 0;
        Player keep;

        do
        {
            passed = true;
            for (int i = 1; i < ranking.Count; i++)
            {
                last = i - 1;

                if (ranking[i].score > ranking[last].score)
                {
                    passed = false;
                    keep = ranking[i];

                    ranking[i] = ranking[last];
                    ranking[last] = keep;
                }

            }
        }
        while (!passed);
    }

    public void GetList(List<Player> newRank)
    {
        for (int i = 0;i < newRank.Count;i++)
        {
            if (i >= ranking.Count)
            {
                ranking.Add(newRank[i]);
            }
            else
            {
                ranking[i] = newRank[i];
            }
        }
    }

}

[System.Serializable]
public class Player
{
    public string name;
    public int score;

    public Player(string name, int score)
    {
        this.name = name;
        this.score = score;
    }

}
