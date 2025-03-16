using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapon : EquipedWeapon
{
    [SerializeField]
    private MeleeAttackCollider attackCollider;
    [SerializeField]
    private BoxCollider2D attackRangeCollider;
    [SerializeField]
    private ContactFilter2D contactFilter;

    private Animator animator;

    private int attackHash;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        attackHash = Animator.StringToHash("Attack");
    }

    private void Start()
    {
        GamePlayManager.Instance.playerChar.WeaponSystem.OnAddWeapon += SetDamage;
        attackCollider.SetMeleeDamage(itemData.MaxAtk);
    }

    public override void DoAttack(Quaternion weaponPivot)
    {
        animator.SetTrigger(attackHash);
    }

    public void SetDamage(int weaponId, int count)
    {
        if(weaponId != itemData.ItemID)
        {
            return;
        }

        float damage = itemData.MaxAtk + itemData.MaxAtk * itemData.MaxAtkMul * count;
        attackCollider.SetMeleeDamage(damage);
    }
}
