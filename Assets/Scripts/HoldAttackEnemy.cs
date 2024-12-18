using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldAttackEnemy : EnemyBehaviour
{
    private State currentState = State.Idle;
    private Coroutine holdCoroutine;

    public float scoreRate;

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

        while (timer < notesDuration[0])
        {
            GameManager.instance.AddScore(4); // Bad check, adds 10 points
            timer += scoreRate; // Adds score every 0.2 seconds
            yield return new WaitForSeconds(scoreRate);
        }

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
