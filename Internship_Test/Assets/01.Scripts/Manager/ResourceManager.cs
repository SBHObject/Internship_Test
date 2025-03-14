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
    Character,
    UI,
    None
}

public class ResourceManager : PersistentSingleton<ResourceManager>
{
    private Dictionary<string, object> resourcePool = new Dictionary<string, object>();

    public T LoadResource<T> (string key, EMajorType majorType, ESubType subType = ESubType.None)
    {
        StringBuilder stringBuilder = new StringBuilder();

        //��ȯ�� ��ü
        T returnAsset;

        stringBuilder.Append(majorType);
        stringBuilder.Append(subType == ESubType.None ? "" : $"/{subType}");
        stringBuilder.Append($"/{key}");

        //���ҽ�Ǯ ��ųʸ� Ȯ��
        if (!resourcePool.ContainsKey(stringBuilder.ToString()))
        {
            var targetResource = Resources.Load (stringBuilder.ToString(), typeof(T));

            if(targetResource == null)
            {
                return default;
            }

            resourcePool.Add(stringBuilder.ToString(), targetResource);
        }

        //��� ��ȯ
        returnAsset = (T)resourcePool[stringBuilder.ToString()];
        return returnAsset;
    }
}
