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

    private float timer = 0f;

    private void Start()
    {
        GamePlayManager.Instance.playerChar.WeaponSystem.OnAddWeapon += SetDamage;
        attackCollider.SetMeleeDamage(itemData.MaxAtk);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if(itemData.AtkSpeed <= timer)
        {
            List<Collider2D> search = new List<Collider2D>();
            var targets = Physics2D.OverlapCollider(attackRangeCollider, contactFilter, search);

            if (targets > 0)
            {
                DoAttack();
                timer = 0f;
            }
        }
    }

    public override void DoAttack()
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
