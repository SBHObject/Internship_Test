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
        //������ƮǮ�� �ֱ�
    }

    public override void OnExit()
    {
        //������ƮǮ���� ������
    }
}
