using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RankingSystem : MonoBehaviour
{

    public static RankingSystem instance;

    public string currentPlayer;
    public List<Player> ranking;

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
        UpdateRankList();
    }

    public void AddRank(string name, int score)
    {
        Player p = new Player(name, score);
        ranking.Add(p);

        UpdateRankList();

    }

    public void UpdateRank(string name, int score)
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
            ranking[ind].score = score;
        }
        else
        {
            AddRank(name, score);
        }

        UpdateRankList();

    }

    public void UpdateRankList()
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
