using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class HoldAttackEnemy : EnemyBehaviour
{
    private State currentState = State.Idle;
    private Coroutine holdCoroutine;

    public float scoreRate;

    public SpriteRenderer bar;
    private float barSize;
    private float barTime;
    private bool attacked;


    public override void Start()
    {
        base.Start();

        barSize = notesDuration[0] * (20 / 5);
        bar.size = new Vector2(barSize, 1);
    }

    public override void Update()
    {
        base.Update();

        if (attacked)
        {
            barTime += Time.deltaTime;

            bar.size = new Vector2(Mathf.Lerp(barSize, 0.1f, barTime / notesDuration[0]), 1);
        }

    }
    public override void NextState(PlayerInputs input, float precision)
    {
        if (currentState == State.Idle)
        {
            if (input == PlayerInputs.Attack)
            {
                currentState = State.Hold;
                holdCoroutine = StartCoroutine(Hold(precision));
            }
        }
        else if (currentState == State.Hold)
        {
            if (input == PlayerInputs.Release)
            {
                StopCoroutine(holdCoroutine); // Stops coroutine responsible for adding score while holding
                AudioController.instance.StopEnemySounds(); // Fades out enemy sounds
                currentState = State.Death;
                Death(precision);
            }
        }
    }

    public IEnumerator Hold(float precision)
    {
        GameManager.instance.AddScore(precision);
        AudioController.instance.PlayEnemySounds(notesDuration[0]);
        transform.parent = playerTransform; // Makes the enemy move with the player
        float timer = 0f;
        attacked = true;
        animator.SetTrigger("Attacked");

        while (timer < notesDuration[0])
        {
            GameManager.instance.score += 10; // Bad check, adds 10 points
            GameManager.instance.UpdateScoreBar(10);
            timer += scoreRate; // Adds score every 0.2 seconds
            yield return new WaitForSeconds(scoreRate);
        }

        bar.size = new Vector2(0, 1);
        NextState(PlayerInputs.Release, 0);
        yield break;
    }

    enum State
    {
        Idle = 0,
        Hold = 1,
        Death = 2
    }
}
