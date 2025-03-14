using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/ItemData")]
public class ItemDataSO : ScriptableObject
{
    [Header("Base Data")]
    public int ItemID;
    public string Name;
    public string Description;
    public int UnlockLevel;

    [Header("Consumables")]
    public int MaxHP;
    public float MaxHpMul;
    public int MaxMp;
    public float MaxMpMul;

    [Header("Weapons")]
    public int MaxAtk;
    public float MaxAtkMul;
    public float AtkSpeed;
    public float AtkRange;

    [Header("Exps")]
    public int Exp;
    public float ExpMul;

    [Header("Special")]
    public int Status;
}
