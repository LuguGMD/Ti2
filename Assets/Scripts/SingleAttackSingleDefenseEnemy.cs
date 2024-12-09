using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SingleAttackSingleDefenseEnemy : EnemyBehaviour
{
    public AnimationCurve hitAnimCurve;

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

        // Hit animation
        var sequence = DOTween.Sequence();
        sequence.Append(transform.DOJump(
            endValue: new Vector3(nextPositions[0], transform.position.y, transform.position.z),
            jumpPower: 3,
            numJumps: 1,
            duration: 1f).SetEase(hitAnimCurve));

        sequence.OnComplete(() =>
        {
            transform.DOShakeScale(0.2f, 1.2f);
        });
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
        Death = 2
    }
}
