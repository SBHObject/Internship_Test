using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestItem : ItemObject, IInteractable
{
    public GameObject effect;

    public void SetItemData(ItemDataSO dataSO)
    {
        itemData = dataSO;
        GetObject();
    }

    public void OnInteracte(PlayerCharacter player)
    {
        player.WeaponSystem.AddWeapon(itemData.ItemID);
        ObjectPoolingManager.Instance.ReleaseToPool(Key, Pool);
    }

    public void ShowInteracteEffect()
    {
        effect.SetActive(true);
    }

    public void HideInteracteEffect()
    {
        effect.SetActive(false);
    }
}
