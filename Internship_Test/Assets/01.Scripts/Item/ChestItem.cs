using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : DropItem, IInteractable
{
    public void SetItemData(ItemDataSO dataSO)
    {
        itemData = dataSO;
        GetObject();
    }

    public override void ItemEffect()
    {
        //TODO : 무기 획득, 상자 열리는 연출
    }

    public void OnInteracte()
    {
        ItemEffect();
    }
}
