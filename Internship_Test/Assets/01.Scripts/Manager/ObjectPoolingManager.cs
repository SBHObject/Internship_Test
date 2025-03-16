using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{
    //오브젝트 풀
    public Dictionary<string, IObjectPool<ObjectPoolable>> poolDictinary = new Dictionary<string, IObjectPool<ObjectPoolable>>();
    //풀링 오브젝트의 부모 오브젝트
    private Dictionary<string, GameObject> poolPerentsDictinary = new Dictionary<string, GameObject>();
    //풀링 오브젝트의 최대 생성 갯수
    private Dictionary<string, int> poolMaxCount = new Dictionary<string, int>();
    //생성된 오브젝트 갯수
    private Dictionary<string, int> createdObjectCount = new Dictionary<string, int>();

    private readonly int monsterPoolCount = 10;
    private readonly int itemPoolCount = 10;

    private ObjectPoolable objectToCreate;

    private readonly Vector2 createPosition = new Vector2(20f, 20f);

    //오브젝트풀에서 꺼내기
    public ObjectPoolable GetFromPool(string poolKey, Vector2 vector)
    {
        if(!poolDictinary.ContainsKey(poolKey))
        {
            return null;
        }

        var returnObject = poolDictinary[poolKey].Get();
        returnObject.GetObject();
        
        if (returnObject != null)
        {
            returnObject.SetPosition(vector);
            returnObject.Pool = returnObject;
            returnObject.SetKey(poolKey);
        }

        return returnObject;
    }

    //오브젝트풀에 넣기
    public void ReleaseToPool(string poolKey, ObjectPoolable poolObject)
    {
        if(poolDictinary.ContainsKey(poolKey))
        {
            poolObject.ReleaseObject();
            poolDictinary[poolKey].Release(poolObject);
        }
    }

    //몬스터 풀 생성
    public List<string> CreateMonsterPool()
    {
        var monsters = ResourceManager.Instance.LoadAllResources<Enemy>(EMajorType.Prefab, ESubType.Enemy);

        List<string> returnKey = new List<string>();
        for(int i = 0; i < monsters.Count; i++)
        {
            CreatePool(monsters[i], monsterPoolCount, monsterPoolCount);
            returnKey.Add(monsters[i].gameObject.name);
        }

        return returnKey;
    }

    //아이템 풀 생성
    public List<string> CreateItemPool()
    {
        var items = ResourceManager.Instance.LoadAllResources<ItemObject>(EMajorType.Prefab, ESubType.Item);

        List<string> returnKey = new List<string>();
        for(int i = 0; i < items.Count; i++)
        {
            CreatePool(items[i], itemPoolCount, 200);
            returnKey.Add(items[i].gameObject.name);
        }

        return returnKey;
    }

    //총알 풀 생성
    public List<string> CreateBulletPool()
    {
        var bullet = ResourceManager.Instance.LoadAllResources<ProjectileBase>(EMajorType.Prefab,ESubType.Bullet);

        List<string> returnKey = new List<string>();
        for(int i = 0; i < bullet.Count; i++)
        {
            CreatePool(bullet[i], 30, 100);
            returnKey.Add(bullet[i].gameObject.name);
        }

        return returnKey;
    }

    private void CreatePool(ObjectPoolable poolObject, int defaultCount, int maxCount)
    {
        if (poolDictinary.ContainsKey(poolObject.gameObject.name))
        {
            return;
        }

        objectToCreate = poolObject;

        //오브젝트 풀 생성
        IObjectPool<ObjectPoolable> newPool = new ObjectPool<ObjectPoolable>(
            CreatePoolObject,
            OnGetPoolObject,
            OnReleasePoolObject,
            OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity : defaultCount,
            maxSize : maxCount
            );

        poolDictinary.Add(poolObject.gameObject.name, newPool);
        poolMaxCount.Add(poolObject.gameObject.name, maxCount);
        createdObjectCount.Add(poolObject.gameObject.name, 0);

        //기본값만큼 오브젝트 생성
        List<ObjectPoolable> tempList = new List<ObjectPoolable>();
        for(int i = 0; i < defaultCount; i++)
        {
            ObjectPoolable instance = newPool.Get();
            instance.SetKey(poolObject.gameObject.name);
            instance.SetPosition(createPosition);
            tempList.Add(instance);
        }

        foreach(var instance in tempList)
        {
            ReleaseToPool(poolObject.gameObject.name, instance);
        }
    }

    //오브젝트 풀에 들어가는 오브젝트 생성시 작동
    private ObjectPoolable CreatePoolObject()
    {
        if (createdObjectCount[objectToCreate.gameObject.name] >= poolMaxCount[objectToCreate.gameObject.name])
        {
            return null;
        }

        string perentKey = $"{objectToCreate.gameObject.name}Pool";
        if (!poolPerentsDictinary.ContainsKey(perentKey))
        {
            poolPerentsDictinary.Add(perentKey, new GameObject(perentKey));
        }

        ObjectPoolable newObject = Instantiate(objectToCreate, poolPerentsDictinary[perentKey].transform);
        createdObjectCount[objectToCreate.gameObject.name]++;

        return newObject;
    }

    //풀에서 오브젝트 꺼내기
    private void OnGetPoolObject(ObjectPoolable poolObject)
    {
        poolObject?.GetObject();
    }

    //풀에 오브젝트 넣기
    private void OnReleasePoolObject(ObjectPoolable poolObject)
    {
        poolObject?.ReleaseObject();
    }

    //풀 오브젝트 파괴
    private void OnDestroyPoolObject(ObjectPoolable poolObject)
    {
        poolObject?.DestroyObject();

        if (poolObject != null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(poolObject.gameObject.name);
            stringBuilder.Replace("(Clone)", "");
            createdObjectCount[stringBuilder.ToString()]--;
        }
    }
}
