using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ObjectPoolable, IDamageable
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
    public PlayerCharacter Player => GamePlayManager.Instance.playerChar;
    public float ToPlayerDistance { get; private set; }
    public Vector2 ToPlayerDir { get; private set; }

    //���ݴ�� ��ġ �˻� �ֱ� ���� �ʵ�
    private float distanceCheckTime = 0.5f;
    private float checkTimer = 0;

    //������ ��� ��ġ
    [SerializeField]
    private Transform dropPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stateMachine = new EnemyStateMachine(this);
        monsterCollider = GetComponent<BoxCollider2D>();

        Status = new EnemyStatus(this);
    }

    private void Start()
    {
        if (GamePlayManager.Instance.IsGameStart == false) return;
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

    //���� �õ�(���Ÿ�, �ٰŸ� ���� ���� -> �ڽ� Ŭ����)
    public virtual void TryAttack()
    {

    }

    public void TakeDamage(float damage)
    {
        //ü���� 0���� �۾����� ���ó��
        if(Status.TakeDamage(damage) <= 0)
        {
            stateMachine.ChangeState(stateMachine.DeathState);
        }
    }

    //������ƮǮ���� ������
    public override void GetObject()
    {
        Status.SetMonsterStatus();
        gameObject.SetActive(true);
    }

    public override void SetPosition(Vector2 vector)
    {
        base.SetPosition(vector);
        CheckDistance();

        stateMachine.ChangeState(stateMachine.ChaseState);

        rb.WakeUp();
    }

    public override void ReleaseObject()
    {
        rb.Sleep();
        base.ReleaseObject();
        stateMachine.ChangeState(stateMachine.IdelState);
    }

    public void DropItem()
    {
        GamePlayManager.Instance.DropManager.DecideDropItem(Data.DropItem, dropPosition.position);
    }
}
