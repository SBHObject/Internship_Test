using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    //�����Ǵ� �Ŵ���
    public ItemDropManager DropManager { get; private set; }

    public PlayerCharacter playerChar { get; private set; }

    public event Action OnGameStartEvent;
    public bool IsGameStart { get; private set; }

    //�÷��� �ð�
    public float GamePlayTime { get; private set; }
    //���� ����
    public int StatusMultiply { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        DropManager = new ItemDropManager();

        IsGameStart = false;
        GamePlayTime = 0;
        StatusMultiply = 1;
    }

    public void Start()
    {
        //SetGamePlay();
    }

    private void Update()
    {
        if(IsGameStart)
        {
            GamePlayTime += Time.deltaTime;
            if(GamePlayTime >= 60)
            {
                //60�ʸ��� ���ݹ��� 1����
                StatusMultiply += 1;
                GamePlayTime = 0;
            }
        }
    }

    public void GameStart()
    {
        OnGameStartEvent?.Invoke();
        IsGameStart = true;
    }

    public void SetGamePlay()
    {
        PlayerCharacter player = ResourceManager.Instance.LoadResource<PlayerCharacter>("PlayerCharacter", EMajorType.Prefab, ESubType.Player);

        playerChar = Instantiate(player, Vector3.zero, Quaternion.identity);

        Enemy monster = ResourceManager.Instance.LoadResource<Enemy>("Zombie", EMajorType.Prefab, ESubType.Enemy);
        Instantiate(monster, Vector3.zero, Quaternion.identity);
    }
}
