using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public GameObject collectableEffect;
    public float[] notesDuration;
    public float[] nextPositions;

    public abstract void NextState(PlayerInputs input, float precision);

    /*public void Damage(float precision)
    {
        AudioController.instance.PlayEnemySounds(noteDuration); // Plays the music when the enemy is hit

        GameManager.instance.AddPoint();
        GameManager.instance.AddScore(precision);
        Instantiate(collectableEffect, transform.position, Quaternion.identity);
        Destroy(gameObject); // Kills enemy if health reaches 0
    }*/

    public virtual void Death(float precision)
    {
        GameManager.instance.AddPoint();
        GameManager.instance.AddScore(precision);
        Instantiate(collectableEffect, transform.position, Quaternion.identity);
        Destroy(gameObject); // Kills enemy if health reaches 0
    }
}
