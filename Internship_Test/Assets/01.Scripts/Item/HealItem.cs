using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealItem : DropItem
{
    public override void ItemEffect(PlayerCharacter player)
    {
        player.Status.TakeHeal(itemData.MaxHP);
        ObjectPoolingManager.Instance.ReleaseToPool(Key, Pool);
    }
}
