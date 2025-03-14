using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private MonsterDataSO data;
    public MonsterDataSO Data { get { return data; } }

    private EnemyStateMachine stateMachine;

    public Rigidbody2D rb {  get; private set; }

    public PlayerCharacter Player { get; private set; }

    public float ToPlayerDistance { get; private set; }
    public Vector2 ToPlayerDir { get; private set; }

    private float distanceCheckTime = 0.5f;
    private float checkTimer = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        Player = GamePlayManager.Instance.playerChar;
        ActiveMonster();
        CheckDistance();
    }

    private void Update()
    {
        if (stateMachine.CurrentState == stateMachine.ChaseState || stateMachine.CurrentState == stateMachine.AttackState)
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= distanceCheckTime)
            {
                CheckDistance();
                checkTimer = 0;
            }
        }

        stateMachine.Update(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    public void CheckDistance()
    {
        ToPlayerDir = Player.transform.position - transform.position;
        ToPlayerDistance = ToPlayerDir.sqrMagnitude;
    }

    public virtual void TryAttack()
    {

    }

    public void SetTarget()
    {
        Player = GamePlayManager.Instance.playerChar;
    }

    private void ActiveMonster()
    {
        stateMachine.ChangeState(stateMachine.ChaseState);
    }
}
