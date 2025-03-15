using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPoolable : MonoBehaviour
{
    public string Key { get; private set; }
    public ObjectPoolable Pool { get; set; }

    public abstract void GetObject();

    public virtual void SetPosition(Vector2 vector)
    {
        transform.position = vector;
    }

    public virtual void ReleaseObject()
    {
        gameObject.SetActive(false);
        Pool = null;
    }

    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }

    public void SetKey(string dictinaryKey)
    {
        Key = dictinaryKey;
    }
}
