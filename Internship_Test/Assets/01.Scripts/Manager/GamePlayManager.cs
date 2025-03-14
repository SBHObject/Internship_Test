using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayManager : Singleton<GamePlayManager>
{
    public PlayerCharacter playerChar { get; private set; }

    public void Start()
    {
        SetGamePlay();
    }

    public void SetGamePlay()
    {
        PlayerCharacter player = ResourceManager.Instance.LoadResource<PlayerCharacter>("PlayerCharacter", EMajorType.Prefab, ESubType.Character);

        playerChar = Instantiate(player, Vector3.zero, Quaternion.identity);

        Enemy monster = ResourceManager.Instance.LoadResource<Enemy>("Zombie", EMajorType.Prefab, ESubType.Character);
        Instantiate(monster, Vector3.zero, Quaternion.identity);
    }
}
