using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "SO/MonsterData")]
public class MonsterDataSO : ScriptableObject
{
    [Header("Base Data")]
    public string MonsterID;
    public string Name;
    public string Description;

    [Header("Combat")]
    public int Attack;
    public float AttackMul;
    public int MaxHP;
    public float MaxHPMul;
    public int AttackRange;
    public float AttackRangeMul;
    public float AttackSpeed;
    public float MoveSpeed;

    [Header("Drops")]
    public int MinExp;
    public int MaxExp;
    public int[] DropItem;
}
