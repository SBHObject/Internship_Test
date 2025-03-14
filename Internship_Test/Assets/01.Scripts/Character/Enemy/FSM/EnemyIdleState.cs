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
        Debug.Log("대기상태");
        enemy.rb.velocity = Vector2.zero;
    }

    public override void OnUpdate(float deltaTime)
    {
        if(GamePlayManager.Instance.playerChar.IsAlive == true)
        {
            stateMachine.ChangeState(stateMachine.ChaseState);
        }
    }

    public override void OnExit()
    {
        enemy.SetTarget();
    }
}
