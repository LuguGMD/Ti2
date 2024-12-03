using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleDefenseSingleAttackEnemy : EnemyBehaviour
{
    private State currentState = State.Idle;
    private Animator animator;

    public override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    public override void NextState(PlayerInputs input, float precision)
    {
        if (currentState == State.Idle)
        {
            if (input == PlayerInputs.Defend)
            {
                currentState = State.Defended;
                Defended(precision);
            }
        }
        else if (currentState == State.Defended)
        {
            if (input == PlayerInputs.Attack)
            {
                currentState = State.Death;
                Death(precision);
            }
        }
    }

    public void Defended(float precision)
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

    public override void Update()
    {
        base.Update();
        if (transform.position.x - (playerTransform.position.x) <= attackDistance)
        {
            animator.SetTrigger("EnableAttack");
        }
    }

    private enum State
    {
        Idle = 0,
        Defended = 1,
        Death = 2
    }
}
