using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int health = 1;
    public GameObject collectableEffect;

    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            GameManager.instance.AddPoint();
            Instantiate(collectableEffect, transform.position, Quaternion.identity);
            Destroy(gameObject); // Kills enemy if health reaches 0
        }
    }
}
