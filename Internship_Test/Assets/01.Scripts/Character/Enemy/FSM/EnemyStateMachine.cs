using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : StateMachine
{
    public Enemy Enemy { get; }

    //���µ� ����
    public EnemyChaseState ChaseState { get; }
    public EnemyAttackState AttackState { get; }
    public EnemyDeathState DeathState { get; }
    public EnemyIdleState IdleState { get; }

    public EnemyStateMachine(Enemy enemy)
    {
        Enemy = enemy;

        ChaseState = new EnemyChaseState(this, Enemy);
        AttackState = new EnemyAttackState(this, Enemy);
        DeathState = new EnemyDeathState(this, Enemy);
        IdleState = new EnemyIdleState(this, Enemy);
    }
}
