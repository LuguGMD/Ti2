using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackSingleDefenseEnemy : EnemyBehaviour
{
    private State currentState = State.Idle;

    public override void NextState(PlayerInputs input, float precision)
    {
        if (currentState == State.Idle)
        {
            if (input == PlayerInputs.Attack)
            {
                currentState = State.Attacked;
                Attacked(precision);
            }
        }
        else if (currentState == State.Attacked)
        {
            if (input == PlayerInputs.Defend)
            {
                currentState = State.Death;
                Death(precision);
            }
        }
    }

    public void Attacked(float precision)
    {
        AudioController.instance.PlayEnemySounds(notesDuration[0]); // Plays the music when the enemy is hit

        GameManager.instance.AddPoint();
        GameManager.instance.AddScore(precision);
        transform.localPosition = new Vector3(nextPositions[0], transform.position.y, transform.position.x); // Replace with hit animation
    }

    public override void Death(float precision)
    {
        AudioController.instance.PlayEnemySounds(notesDuration[1]);
        base.Death(precision);
    }

    private enum State
    {
        Idle = 0,
        Attacked = 1,
        Death
    }
}
