using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class ProjectileBase : ObjectPoolable
{
    private Rigidbody2D rb;

    private float damage;

    private readonly float shotPower = 10f;

    private bool canAttack = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.right * shotPower;
    }

    public override void GetObject()
    {
        rb.WakeUp();
        gameObject.SetActive(true);
    }

    public override void ReleaseObject()
    {
        canAttack = false;
        rb.velocity = Vector3.zero;
        rb.Sleep();
        base.ReleaseObject();
    }

    public void SetData(float _damage, bool active)
    {
        damage = _damage;
        canAttack = active;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canAttack == false) return;

        if(collision.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);

            ObjectPoolingManager.Instance.ReleaseToPool(Key, Pool);
        }

        if(collision.tag == "Wall")
        {
            ObjectPoolingManager.Instance.ReleaseToPool(Key, Pool);
        }
    }
}
