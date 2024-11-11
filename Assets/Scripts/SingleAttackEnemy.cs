using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackEnemy : EnemyBehaviour
{
    private State currentState = State.Idle;

    public override void NextState(PlayerInputs input, float precision)
    {
        if (currentState == State.Idle && input == PlayerInputs.Attack)
        {
            currentState = State.Death;
            Death(precision);
        }
    }

    public override void Death(float precision)
    {
        AudioController.instance.PlayEnemySounds(notesDuration[0]); // Plays the music when the enemy is hit
        base.Death(precision); // Calls EnemyBehaviour's Death method;
    }

    enum State
    {
        Idle = 0,
        Death = 1
    }
}
