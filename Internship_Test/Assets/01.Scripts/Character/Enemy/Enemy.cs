using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public MonsterDataSO Data { get; private set; }
    private EnemyStateMachine stateMachine;

    public Rigidbody2D rb {  get; private set; }

    public PlayerCharacter player { get; private set; }

    public float toPlayerDistance { get; private set; }
    public Vector2 toPlayerDir { get; private set; }

    private float distanceCheckTime = 0.5f;
    private float checkTimer = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new EnemyStateMachine(this);
    }

    private void Start()
    {
        CheckDistance();
    }

    private void Update()
    {
        stateMachine.Update(Time.deltaTime);

        checkTimer += Time.deltaTime;
        if(checkTimer >= distanceCheckTime)
        {
            CheckDistance();
            checkTimer = 0;
        }
    }

    private void CheckDistance()
    {
        toPlayerDir = player.transform.position - transform.position;
        toPlayerDistance = toPlayerDir.sqrMagnitude;
    }

    public virtual void TryAttack()
    {

    }
}
