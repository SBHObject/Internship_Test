using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropItem : ItemObject
{
    //������ ȹ��� ȿ��
    public abstract void ItemEffect(PlayerCharacter player);
}
