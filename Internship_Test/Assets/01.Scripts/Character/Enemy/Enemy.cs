using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : ObjectPoolable, IDamageable
{
    //몬스터 데이터
    [SerializeField]
    private MonsterDataSO data;
    public MonsterDataSO Data { get { return data; } }
    public EnemyStatus Status { get; private set; }

    //몬스터 움직임 관련 필드
    private EnemyStateMachine stateMachine;
    [SerializeField]
    private SpriteRenderer sprite;
    public SpriteRenderer Sprite { get { return sprite; } }
    public Rigidbody2D rb {  get; private set; }
    private BoxCollider2D monsterCollider;

    //공격대상 관련 필드
    public PlayerCharacter Player => GamePlayManager.Instance.playerChar;
    public float ToPlayerDistance { get; private set; }
    public Vector2 ToPlayerDir { get; private set; }

    //공격대상 위치 검색 주기 관련 필드
    private float distanceCheckTime = 0.5f;
    private float checkTimer = 0;

    //아이템 드롭 위치
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

    //공격 시도(원거리, 근거리 별개 구현 -> 자식 클래스)
    public virtual void TryAttack()
    {

    }

    public void TakeDamage(float damage)
    {
        //체력이 0보다 작아지면 사망처리
        if(Status.TakeDamage(damage) <= 0)
        {
            stateMachine.ChangeState(stateMachine.DeathState);
        }
    }

    //오브젝트풀에서 꺼내기
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
