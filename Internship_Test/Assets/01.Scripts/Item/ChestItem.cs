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
        //TODO : ���� ȹ��, ���� ������ ����
    }

    public void OnInteracte()
    {
        ItemEffect();
    }
}
