using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager
{
    private Dictionary<int, int> unlockLevels = new Dictionary<int, int>();

    public ItemDropManager()
    {
        ObjectPoolingManager.Instance.CreateItemPool();
        List<ItemDataSO> data = ResourceManager.Instance.LoadAllResources<ItemDataSO>(EMajorType.Data, ESubType.Item);

        foreach(ItemDataSO item in data)
        {
            unlockLevels.Add(item.ItemID, item.UnlockLevel);
        }
    }

    public List<ItemObject> DecideDropItem(List<int> dropTable)
    {
        List<int> exp = new List<int>();
        List<int> other = new List<int>();

        for(int i = 0; i < dropTable.Count; i++)
        {
            if (dropTable[i] / 10000 == 3)
            {
                exp.Add(i);
            }
            else
            {
                other.Add(i);
            }
        }

        int randomNum = Random.Range(1, 13);

        List<ItemObject> dropItems = new List<ItemObject>();
        dropItems.Add((ItemObject)ObjectPoolingManager.Instance.Get(ExpDrop(randomNum, exp.Count).ToString()));
        
        if(DropOther(randomNum))
        {
            int target = randomNum % other.Count;
            if (unlockLevels[other[target]] >= GamePlayManager.Instance.playerChar.Status.Level)
            {
                dropItems.Add((ItemObject)ObjectPoolingManager.Instance.Get(other[target].ToString()));
            }
        }

        return dropItems;
    }

    private int ExpDrop(int randNum , int listCount)
    {
        int target = randNum % listCount;

        return 30000 + target;
    }

    private bool DropOther(int randNum)
    {
        if(randNum <= 4)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
