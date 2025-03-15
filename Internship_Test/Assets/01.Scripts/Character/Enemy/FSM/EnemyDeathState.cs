using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathState : State
{
    private readonly float returnPoolTime = 1f;
    private float timer;

    public EnemyDeathState(EnemyStateMachine stateMachine, Enemy enemy) : base(stateMachine, enemy)
    {
    }

    public override void OnEnter()
    {
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
        //오브젝트풀에 넣기
        ObjectPoolingManager.Instance.ReleaseToPool(enemy.Key, enemy);
    }
}
