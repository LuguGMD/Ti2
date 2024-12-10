using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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
    public float attackDistance;

    [SerializeField]
    public BoxCollider bc;
    [SerializeField]
    protected Animator animator;

    public Vector3 deathRotation = new Vector3(60, -45, 30);
    public AnimationCurve deathAnimCurve;
    private bool death = false;

    private float horizontalSpeed;
    private float verticalSpeed;
    private Vector3 startPos;
    private float time = 0f;


    public virtual void Start()
    {
        playerTransform = GameObject.Find("Player").GetComponent<Transform>();
        l.enabled = GameManager.instance.powerupActive;

        horizontalSpeed = Random.Range(36, 44);
        verticalSpeed = Random.Range(8, 10);

        if (iconName != "")
        {
            attackIcon = GameObject.Find(iconName);
        }
    }

    public virtual void Update()
    {
        l.enabled = GameManager.instance.powerupActive;

        if (death)
        {
            transform.position = new Vector3(startPos.x + horizontalSpeed * time, startPos.y + verticalSpeed * deathAnimCurve.Evaluate(time), transform.position.z + 0.5f * time);
            time += Time.deltaTime;
        }

        if (transform.position.x - (playerTransform.position.x) <= attackDistance)
        {
            animator.SetTrigger("EnableAttack");
            if(transform.position.y > 2f)
            {
                transform.DOLocalMoveY(0f, 0.3f);
            }
        }

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
        Destroy(gameObject, 3f); // Kills enemy if health reaches 0
        bc.enabled = false;

        transform.parent = playerTransform;
        animator.SetTrigger("Died");
        death = true;
        startPos = transform.position;

        transform.DORotate(deathRotation, 0.1f).SetLoops(-1, LoopType.Incremental);
        transform.DOScale(0, 2f);

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
