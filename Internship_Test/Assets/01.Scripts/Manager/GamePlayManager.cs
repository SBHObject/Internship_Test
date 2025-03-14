using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public PlayerCharacter playerChar { get; private set; }

    public event Action OnGameStartEvent;
    public bool IsGameStart { get; private set; }

    //플레이 시간
    public float GamePlayTime { get; private set; }
    //스텟 배율
    public int StatusMultiply { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        IsGameStart = false;
        GamePlayTime = 0;
        StatusMultiply = 1;
    }

    public void Start()
    {
        SetGamePlay();
    }

    private void Update()
    {
        if(IsGameStart)
        {
            GamePlayTime += Time.deltaTime;
            if(GamePlayTime >= 60)
            {
                //60초마다 스텟배율 1증가
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
        PlayerCharacter player = ResourceManager.Instance.LoadResource<PlayerCharacter>("PlayerCharacter", EMajorType.Prefab, ESubType.Character);

        playerChar = Instantiate(player, Vector3.zero, Quaternion.identity);

        Enemy monster = ResourceManager.Instance.LoadResource<Enemy>("Zombie", EMajorType.Prefab, ESubType.Character);
        Instantiate(monster, Vector3.zero, Quaternion.identity);
    }
}
