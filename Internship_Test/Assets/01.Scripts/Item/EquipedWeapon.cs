using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipedWeapon : ItemObject
{
    protected float timer = 0f;

    [SerializeField]
    private SpriteRenderer weaponSpriteRender;

    public void FlipSprite(bool flip)
    {
        weaponSpriteRender.flipY = flip;
    }

    //공격 구현
    public abstract void DoAttack(Quaternion weaponPivot);

    public void ShowWeapon()
    {
        gameObject.SetActive(true);
    }

    public void HideWeapon()
    {
        gameObject.SetActive(false);
    }
}
