using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public enum EMajorType
{
    Data,
    Prefab
}

public enum ESubType
{
    Item,
    Enemy,
    Player,
    UI,
    Map,
    Weapon,
    Bullet,
    None
}

public class ResourceManager : PersistentSingleton<ResourceManager>
{
    private Dictionary<string, object> resourcePool = new Dictionary<string, object>();

    public T LoadResource<T> (string key, EMajorType majorType, ESubType subType = ESubType.None)
    {
        StringBuilder stringBuilder = new StringBuilder();

        //반환할 객체
        T returnAsset;

        stringBuilder.Append(majorType);
        stringBuilder.Append(subType == ESubType.None ? "" : $"/{subType}");
        stringBuilder.Append($"/{key}");

        //리소스풀 딕셔너리 확인
        if (!resourcePool.ContainsKey(stringBuilder.ToString()))
        {
            var targetResource = Resources.Load(stringBuilder.ToString(), typeof(T));

            if(targetResource == null)
            {
                return default;
            }

            resourcePool.Add(stringBuilder.ToString(), targetResource);
        }

        //대상 반환
        returnAsset = (T)resourcePool[stringBuilder.ToString()];
        return returnAsset;
    }

    public List<T> LoadAllResources<T>(EMajorType majorType, ESubType subType = ESubType.None)
    {
        StringBuilder stringBuilder = new StringBuilder();

        stringBuilder.Append(majorType);
        stringBuilder.Append(subType == ESubType.None ? "" : $"/{subType}");

        List<T> returnResource = new List<T>();

        if (!resourcePool.ContainsKey(stringBuilder.ToString()))
        {
            List<string> keyList = new List<string>();
            
            object[] loaded = Resources.LoadAll(stringBuilder.ToString(), typeof(T));

            foreach (object obj in loaded)
            {
                StringBuilder key = new StringBuilder();
                key.Append(stringBuilder.ToString());
                key.Append($"/");
                key.Append(obj.ToString());
                key.Replace($" ({obj.GetType()})", "");

                resourcePool.Add(key.ToString(), obj);

                returnResource.Add((T)obj);
                keyList.Add(key.ToString());
            }

            resourcePool.Add(stringBuilder.ToString(), keyList);
        }

        List<string> getkey = (List<string>)resourcePool[stringBuilder.ToString()];
        for(int i = 0; i < getkey.Count; i++)
        {
            returnResource.Add((T)resourcePool[getkey[i]]);
        }

        return returnResource;
    }
}
