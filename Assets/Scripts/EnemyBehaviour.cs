using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int health = 1;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            gameManager.AddPoint();
            Destroy(gameObject); // Kills enemy if health reaches 0
        }
    }
}
