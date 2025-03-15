using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : ItemObject
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TakeItem();

        ReleaseObject();
    }

    protected virtual void TakeItem()
    {
        //TODO : æ∆¿Ã≈€ »πµÊ ¡¶¿€
    }
}
