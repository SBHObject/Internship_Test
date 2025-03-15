using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : State
{
    //�÷��̾� ����� �� ���·�
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
