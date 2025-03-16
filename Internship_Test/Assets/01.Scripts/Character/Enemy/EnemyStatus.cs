using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus
{
    private Enemy enemy;

    private float maxHp;
    public float MaxHp { get { return maxHp; } }

    public float CurrentHp { get; private set; }

    public float AttackDamage { get; private set; }

    public EnemyStatus(Enemy _enemy)
    {
        enemy = _enemy;
    }

    //���� ������ �������ͽ� ����
    public void SetMonsterStatus()
    {
        maxHp = enemy.Data.MaxHP + enemy.Data.MaxHP * enemy.Data.MaxHPMul * GamePlayManager.Instance.StatusMultiply;

        CurrentHp = maxHp;
        AttackDamage = enemy.Data.Attack + enemy.Data.Attack * enemy.Data.AttackMul * GamePlayManager.Instance.StatusMultiply;
    }

    //���� ���
    public float TakeDamage(float damage)
    {
        CurrentHp = (CurrentHp <= damage) ? 0 : CurrentHp - damage;

        return CurrentHp;
    }
}
