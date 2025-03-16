using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager
{
    //생성 위치
    private List<Transform> monsterSpawnPoints;
    //몬스터풀의 키 값
    private List<string> monsterKey = new List<string>();

    //생성주기
    private readonly float monsterSpawnTime = 1f;
    private float spawnTimer = 0;

    public SpawnManager()
    {
        var mapPrefab = ResourceManager.Instance.LoadResource<MapInfo>("Map", EMajorType.Prefab, ESubType.Map);
        var map = Object.Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);

        monsterSpawnPoints = map.SpawnPoint;

        //플레이어 캐릭터 생성
        PlayerCharacter player = ResourceManager.Instance.LoadResource<PlayerCharacter>("PlayerCharacter", EMajorType.Prefab, ESubType.Player);
        GamePlayManager.Instance.SetPlayerCharacter(Object.Instantiate(player, Vector3.zero, Quaternion.identity));

        monsterKey = ObjectPoolingManager.Instance.CreateMonsterPool();
    }

    //몬스터 스폰
    public void SpawnMonster(float deltaTime)
    {
        spawnTimer += deltaTime;
        if(spawnTimer < monsterSpawnTime)
        {
            return;
        }

        for(int i = 0; i < monsterKey.Count; i++)
        {
            int rand = Random.Range(0, monsterSpawnPoints.Count);

            var created = ObjectPoolingManager.Instance.GetFromPool(monsterKey[i], monsterSpawnPoints[rand].position);
        }

        spawnTimer = 0;
    }
}
