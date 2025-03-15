using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpItem : DropItem
{
    private float expAmount;

    

    public override void GetObject()
    {
        base.GetObject();

        expAmount = itemData.Exp + itemData.Exp * itemData.ExpMul * GamePlayManager.Instance.StatusMultiply;
    }

    public override void ItemEffect()
    {
        GamePlayManager.Instance.playerChar.Status.GetExp(expAmount);
    }
}
