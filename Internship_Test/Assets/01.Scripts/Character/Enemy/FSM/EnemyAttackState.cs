using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : State
{
    private float attackDelay;
    private float attackRange;

    public EnemyAttackState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {
        attackRange = enemy.Data.AttackRange * enemy.Data.AttackRange;
    }

    public override void OnUpdate(float deltaTime)
    {
        attackDelay += deltaTime;

        if(enemy.Data.AttackSpeed <= attackDelay)
        {
            enemy.TryAttack();
            attackDelay = 0;
        }

        if(enemy.ToPlayerDistance > attackRange)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
    }

    public override void OnFixedUpdate()
    {

    }

    public override void OnExit()
    {

    }
}
