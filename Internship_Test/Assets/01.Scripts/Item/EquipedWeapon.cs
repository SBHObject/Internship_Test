using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EquipedWeapon : ItemObject
{
    protected Animator animator;

    protected int attackHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        attackHash = Animator.StringToHash("Attack");
    }

    //공격 구현
    public abstract void DoAttack();

    public void ShowWeapon()
    {
        gameObject.SetActive(true);
    }

    public void HideWeapon()
    {
        gameObject.SetActive(false);
    }
}
