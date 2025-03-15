using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : ObjectPoolable
{
    [SerializeField]
    protected ItemDataSO itemData;

    public override void GetObject()
    {
        gameObject.SetActive(true);
    }
}
