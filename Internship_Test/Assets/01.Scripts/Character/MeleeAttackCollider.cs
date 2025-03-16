using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttackCollider : MonoBehaviour
{
    private float meleeDamage;

    private List<Collider2D> alreadyCheck = new List<Collider2D>();

    private void OnEnable()
    {
        alreadyCheck.Clear();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //�̹� ������ ����� ���̻� �������� ����
        if(alreadyCheck.Contains(collision))
        {
            return;
        }

        if(collision.TryGetComponent(out IDamageable character))
        {
            alreadyCheck.Add(collision);
            character.TakeDamage(meleeDamage);
        }
    }

    public void SetMeleeDamage(float damage)
    {
        meleeDamage = damage;
    }
}
