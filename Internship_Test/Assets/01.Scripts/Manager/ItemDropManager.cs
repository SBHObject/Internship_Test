using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager
{
    private Dictionary<int, int> unlockLevels = new Dictionary<int, int>();

    private readonly int expID = 30000;
    private readonly int dropFactor = 4;

    public ItemDropManager()
    {
        ObjectPoolingManager.Instance.CreateItemPool();
        List<ItemDataSO> data = ResourceManager.Instance.LoadAllResources<ItemDataSO>(EMajorType.Data, ESubType.Item);

        foreach(ItemDataSO item in data)
        {
            unlockLevels.Add(item.ItemID, item.UnlockLevel);
        }
    }

    public void DecideDropItem(List<int> dropTable, Vector2 dropPos)
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
                other.Add(dropTable[i]);
            }
        }

        //랜덤 드롭 결정
        int randomNum = Random.Range(1, 13);

        if (randomNum >= 2)
        {
            //경험치
            ObjectPoolingManager.Instance.GetFromPool(ExpDrop(randomNum, exp.Count).ToString(), SetDropPosition(dropPos));
        }
        
        //경험치 이외의 아이템
        if(DropOther(randomNum))
        {
            int target = randomNum % other.Count;
            if (unlockLevels[other[target]] >= GamePlayManager.Instance.playerChar.Status.Level)
            {
                if (other[target] / 10000 == 1)
                {
                    ChestItem chest = (ChestItem)ObjectPoolingManager.Instance.GetFromPool("Chest", SetDropPosition(dropPos));
                    ItemDataSO data = ResourceManager.Instance.LoadResource<ItemDataSO>(other[target].ToString(), EMajorType.Data, ESubType.Item);
                    chest.SetItemData(data);
                }
                else
                {
                    ObjectPoolingManager.Instance.GetFromPool(other[target].ToString(), SetDropPosition(dropPos));
                }
            }
        }
    }

    //드롭 위치 조정
    private Vector2 SetDropPosition(Vector2 basePos)
    {
        float rand = Random.Range(-0.3f, 0.3f);
        rand = Mathf.Floor(rand * 10f) / 10f;
        return new Vector2(basePos.x + rand, basePos.y + rand);
    }

    private int ExpDrop(int randNum , int listCount)
    {
        int target = randNum % listCount;

        return expID + target;
    }

    private bool DropOther(int randNum)
    {
        if(randNum <= dropFactor)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
