using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    private float attackDelay;

    public EnemyAttackState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {

    }

    public override void OnUpdate(float deltaTime)
    {
        attackDelay += deltaTime;

        if(enemy.Data.AttackSpeed <= attackDelay)
        {
            enemy.TryAttack();
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }
}
