using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int health = 100;
    public Slider healthBar;
    
    private int points;
    public TMP_Text pointsText;

    public void AddPoint()
    {
        points++;
        pointsText.SetText(points.ToString());
    }

    public void Damage(int damage)
    {
        health -= damage;
        healthBar.value = health;
    }
}
