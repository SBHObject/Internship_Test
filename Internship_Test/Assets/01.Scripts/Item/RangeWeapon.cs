using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : EquipedWeapon
{
    [SerializeField]
    private Transform firePoint;
    private float damage;
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private SpriteRenderer weaponSprite;

    private readonly string bulletKey = "PlayerBullet";

    private void Start()
    {
        GamePlayManager.Instance.playerChar.WeaponSystem.OnAddWeapon += SetDamage;
        damage = itemData.MaxAtk;
    }

    public override void DoAttack(Quaternion weaponPivot)
    {
        ProjectileBase bullet = (ProjectileBase)ObjectPoolingManager.Instance.GetFromPool(bulletKey, firePoint.position);

        bullet.SetPosition(firePoint.position);
        bullet.transform.rotation = weaponPivot;
        bullet.SetData(damage);
    }


    public void SetDamage(int weaponId, int count)
    {
        if (weaponId != itemData.ItemID)
        {
            return;
        }

        damage = itemData.MaxAtk + itemData.MaxAtk * itemData.MaxAtkMul * count;
    }
}
