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
        //오브젝트풀에 넣기
    }

    public override void OnExit()
    {
        //오브젝트풀에서 꺼내기
    }
}
