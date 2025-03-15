using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    //플레이어 사망시 이 상태로
    public EnemyIdleState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {
        enemy.rb.velocity = Vector2.zero;
    }

    public override void OnExit()
    {
        
    }
}
