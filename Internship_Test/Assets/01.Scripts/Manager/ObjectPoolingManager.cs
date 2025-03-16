using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Pool;

public class ObjectPoolingManager : Singleton<ObjectPoolingManager>
{
    //������Ʈ Ǯ
    public Dictionary<string, IObjectPool<ObjectPoolable>> poolDictinary = new Dictionary<string, IObjectPool<ObjectPoolable>>();
    //Ǯ�� ������Ʈ�� �θ� ������Ʈ
    private Dictionary<string, GameObject> poolPerentsDictinary = new Dictionary<string, GameObject>();
    //Ǯ�� ������Ʈ�� �ִ� ���� ����
    private Dictionary<string, int> poolMaxCount = new Dictionary<string, int>();
    //������ ������Ʈ ����
    private Dictionary<string, int> createdObjectCount = new Dictionary<string, int>();

    private readonly int monsterPoolCount = 10;
    private readonly int itemPoolCount = 10;

    private ObjectPoolable objectToCreate;

    private readonly Vector2 createPosition = new Vector2(20f, 20f);

    //������ƮǮ���� ������
    public ObjectPoolable GetFromPool(string poolKey, Vector2 vector)
    {
        if(!poolDictinary.ContainsKey(poolKey))
        {
            return null;
        }

        if (poolDictinary[poolKey].CountInactive == 0)
        {
            return null;
        }

        
        var returnObject = poolDictinary[poolKey].Get();
        
        if (returnObject != null)
        {
            returnObject.SetPosition(vector);
            returnObject.Pool = returnObject;
            returnObject.SetKey(poolKey);
        }

        return returnObject;
    }

    //������ƮǮ�� �ֱ�
    public void ReleaseToPool(string poolKey, ObjectPoolable poolObject)
    {
        if(poolDictinary.ContainsKey(poolKey))
        {
            poolObject.ReleaseObject();
            poolDictinary[poolKey].Release(poolObject);
        }
    }

    //���� Ǯ ����
    public List<string> CreateMonsterPool()
    {
        var monsters = ResourceManager.Instance.LoadAllResources<Enemy>(EMajorType.Prefab, ESubType.Enemy);

        List<string> returnKey = new List<string>();
        for(int i = 0; i < monsters.Count; i++)
        {
            CreatePool(monsters[i], monsterPoolCount, monsterPoolCount);
            returnKey.Add(monsters[i].gameObject.name);

            UIManager.Instance.infoUI.CreateMonsterInfoSlot(monsters[i].gameObject.name);
        }

        return returnKey;
    }

    //������ Ǯ ����
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

    //�Ѿ� Ǯ ����
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

        //������Ʈ Ǯ ����
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

        //�⺻����ŭ ������Ʈ ����
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

    //������Ʈ Ǯ�� ���� ������Ʈ ������ �۵�
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

    //Ǯ���� ������Ʈ ������
    private void OnGetPoolObject(ObjectPoolable poolObject)
    {
        poolObject?.GetObject();
    }

    //Ǯ�� ������Ʈ �ֱ�
    private void OnReleasePoolObject(ObjectPoolable poolObject)
    {
        poolObject?.ReleaseObject();
    }

    //Ǯ ������Ʈ �ı�
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
