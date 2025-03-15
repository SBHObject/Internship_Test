using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    private readonly float returnPoolTime = 1f;
    private float timer;

    private int deadHesh = Animator.StringToHash("IsDead");

    public EnemyDeathState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {
        enemy.Animator.SetBool(deadHesh, true);
        enemy.rb.velocity = Vector2.zero;
        enemy.MonsterCollider.enabled = false;
        timer = 0;
        enemy.DropItem();
    }

    public override void OnUpdate(float deltaTime)
    {
        timer += deltaTime;

        if(returnPoolTime <= timer)
        {
            stateMachine.ChangeState(stateMachine.IdleState);
        }
    }

    public override void OnExit()
    {
        enemy.Animator.SetBool(deadHesh, false);
        ObjectPoolingManager.Instance.ReleaseToPool(enemy.Key, enemy.Pool);
    }
}
