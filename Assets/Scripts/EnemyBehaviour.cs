using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehaviour : MonoBehaviour
{
    public GameObject collectableEffect;
    public float[] notesDuration;
    public float[] nextPositions;

    protected Transform playerTransform;

    public Light l;

    private void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        l.enabled = GameManager.instance.powerupActive;
    }

    private void Update()
    {
        l.enabled = GameManager.instance.powerupActive;
    }


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
        Instantiate(collectableEffect, transform.position, Quaternion.identity, playerTransform);
        Destroy(gameObject); // Kills enemy if health reaches 0
    }
}
