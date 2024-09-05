using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public int health = 1;
    public void Damage()
    {
        health--;

        if (health <= 0)
        {
            Destroy(gameObject); // Kills enemy if health reaches 0
        }
    }
}
