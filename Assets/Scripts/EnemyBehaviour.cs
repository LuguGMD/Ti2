using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int health = 1;
    public GameObject collectableEffect;
    public float noteDuration;

    public void Damage(float precision)
    {
        health--;
        AudioController.instance.PlayEnemySounds(noteDuration); // Plays the music when the enemy is hit

        if (health <= 0)
        {
            GameManager.instance.AddPoint();
            GameManager.instance.AddScore(precision);
            Instantiate(collectableEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Kills enemy if health reaches 0
        }
    }
}
