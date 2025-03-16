using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    public EnemyDeathState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {
        enemy.rb.velocity = Vector2.zero;
        enemy.MonsterCollider.enabled = false;
        enemy.DropItem();
    }

    public override void OnExit()
    {
        
    }
}
