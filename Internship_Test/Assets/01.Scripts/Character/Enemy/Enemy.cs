using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    //���� ������
    [SerializeField]
    private MonsterDataSO data;
    public MonsterDataSO Data { get { return data; } }
    public EnemyStatus Status { get; private set; }

    //���� ������ ���� �ʵ�
    private EnemyStateMachine stateMachine;
    [SerializeField]
    private SpriteRenderer sprite;
    public SpriteRenderer Sprite { get { return sprite; } }
    public Rigidbody2D rb {  get; private set; }
    private BoxCollider2D monsterCollider;

    //���ݴ�� ���� �ʵ�
    public PlayerCharacter Player { get; private set; }
    public float ToPlayerDistance { get; private set; }
    public Vector2 ToPlayerDir { get; private set; }

    //���ݴ�� ��ġ �˻� �ֱ� ���� �ʵ�
    private float distanceCheckTime = 0.5f;
    private float checkTimer = 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new EnemyStateMachine(this);
        monsterCollider = GetComponent<BoxCollider2D>();

        Status = new EnemyStatus(this);
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
        if (rb.velocity.x > 0)
        {
            Sprite.flipX = false;
        }
        else if (rb.velocity.x < 0)
        {
            Sprite.flipX = true;
        }
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

    public void TakeDamage(float damage)
    {
        //ü���� 0���� �۾����� ���ó��
        if(Status.TakeDamage(damage) <= 0)
        {
            stateMachine.ChangeState(stateMachine.DeathState);
        }
    }
}
