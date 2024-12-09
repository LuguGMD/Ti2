using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class EnemyBehaviour : MonoBehaviour
{
    [Header("UI")]
    public string iconName = "";
    private GameObject attackIcon;

    [Header("Enemy Properties")]
    public GameObject collectableEffect;
    public float[] notesDuration;
    public float[] nextPositions;

    protected Transform playerTransform;

    public Light l;
    public float attackDistance = 0;

    public AnimationCurve deathAnimCurve;


    public virtual void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        l.enabled = GameManager.instance.powerupActive;
        if (iconName != "")
        {
            attackIcon = GameObject.Find(iconName);
        }
    }

    public virtual void Update()
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

        // Updates defeat beasts achivements
        for (int i = 4; i <=6; i++)
        {
            AchievementSystem.instance.UpdateAchievement(i, 1); // Achivement Id = 4 -> Defeat 100 beasts achivement
                                                                // Achivement Id = 5 -> Defeat 300 beasts achivement
                                                                // Achivement Id = 6 -> Defeat 1000 beasts achivement
        }
    }

    public void EnableIcon()
    {
        attackIcon.GetComponent<Image>().enabled = true;
    }

    public void DisableIcon()
    {
        attackIcon.GetComponent<Image>().enabled = false;
    }
}
