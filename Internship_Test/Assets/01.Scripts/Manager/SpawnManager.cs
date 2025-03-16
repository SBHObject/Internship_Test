using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class SpawnManager
{
    //���� ��ġ
    private List<Transform> monsterSpawnPoints;
    //����Ǯ�� Ű ��
    private List<string> monsterKey = new List<string>();

    //�����ֱ�
    private readonly float monsterSpawnTime = 1f;
    private float spawnTimer = 0;

    public SpawnManager()
    {
        var mapPrefab = ResourceManager.Instance.LoadResource<MapInfo>("Map", EMajorType.Prefab, ESubType.Map);
        var map = Object.Instantiate(mapPrefab, Vector3.zero, Quaternion.identity);

        monsterSpawnPoints = map.SpawnPoint;

        //�÷��̾� ĳ���� ����
        PlayerCharacter player = ResourceManager.Instance.LoadResource<PlayerCharacter>("PlayerCharacter", EMajorType.Prefab, ESubType.Player);
        GamePlayManager.Instance.SetPlayerCharacter(Object.Instantiate(player, Vector3.zero, Quaternion.identity));

        monsterKey = ObjectPoolingManager.Instance.CreateMonsterPool();
    }

    //���� ����
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
