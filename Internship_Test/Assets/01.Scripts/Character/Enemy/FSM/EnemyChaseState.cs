using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : State
{
    public EnemyChaseState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {
        enemy.CheckDistance();
    }

    public override void OnUpdate(float deltaTime)
    {
        ChangeAttack();
    }

    public override void OnFixedUpdate()
    {
        MoveEnemy();
    }

    public override void OnExit()
    {
        enemy.rb.velocity = Vector2.zero;
    }

    private void ChangeAttack()
    {
        float attackRange = enemy.Data.AttackRange * enemy.Data.AttackRange;
        if(attackRange >= enemy.ToPlayerDistance)
        {
            stateMachine.ChangeState(stateMachine.AttackState);
        }
    }

    private void MoveEnemy()
    {
        enemy.rb.velocity = 2 * enemy.Data.MoveSpeed * enemy.ToPlayerDir.normalized;
    }
}
