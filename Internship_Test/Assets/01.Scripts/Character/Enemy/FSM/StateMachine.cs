using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    //현재 상태를 관리하는 stateMachine
    protected EnemyStateMachine stateMachine;
    //이 상태머신을 가지는 클래스
    protected Enemy enemy;

    public State(EnemyStateMachine stateMachine, Enemy enemy)
    {
        this.stateMachine = stateMachine;
        this.enemy = enemy;
    }



    /// <summary>
    /// 상태 진입시 작동
    /// </summary>
    public virtual void OnEnter() { }

    /// <summary>
    /// 상태에서 나갈때 작동
    /// </summary>
    public virtual void OnExit() { }

    /// <summary>
    /// 해당 상태에서 업데이트
    /// </summary>
    public virtual void OnUpdate(float deltaTime) { }

    public virtual void OnFixedUpdate() { }

}

public class StateMachine
{
    //현재 상태
    private State currentState;
    public State CurrentState => currentState;

    /// <summary>
    /// 상태 변경
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
