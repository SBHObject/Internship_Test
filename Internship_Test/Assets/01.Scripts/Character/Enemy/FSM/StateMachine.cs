using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //���� ���¸� �����ϴ� stateMachine
    protected EnemyStateMachine stateMachine;
    //�� ���¸ӽ��� ������ Ŭ����
    protected Enemy enemy;

    public State(EnemyStateMachine stateMachine, Enemy enemy)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
    }



    /// <summary>
    /// ���� ���Խ� �۵�
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// ���¿��� ������ �۵�
    /// </summary>
    public virtual void OnExit() { }

    /// <summary>
    /// �ش� ���¿��� ������Ʈ
    /// </summary>
    public virtual void OnUpdate(float deltaTime) { }

    public virtual void OnFixedUpdate() { }

}

public class StateMachine
{
    //���� ����
    private State currentState;
    public State CurrentState => currentState;

    /// <summary>
    /// ���� ����
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(State state)
    {
        currentState?.OnExit();
        currentState = state;
        currentState?.OnEnter();
    }

    public void Update(float deltaTime)
    {
        currentState?.OnUpdate(deltaTime);
    }

    public void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
}
