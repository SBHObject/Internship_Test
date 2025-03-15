using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField]
    private MeleeAttackCollider attackCollider;

    public override void TryAttack()
    {
        attackCollider.gameObject.SetActive(true);
    }

    public void EndAttack()
    {
        attackCollider.gameObject.SetActive(false);
    }

    public override void GetObject()
    {
        base.GetObject();
        attackCollider.SetMeleeDamage(Status.AttackDamage);
    }
}
