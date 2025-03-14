using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class ObjectPoolable : MonoBehaviour
{
    public abstract void GetObject();

    public virtual void ReleaseObject()
    {
        gameObject.SetActive(false);
    }

    public virtual void DestroyObject()
    {
        Destroy(gameObject);
    }
}
