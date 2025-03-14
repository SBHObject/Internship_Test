using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
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
    public List<int> DropItem;
}

[Serializable]
[XmlRoot("MonsterData")]
public class MonsterData
{
    [XmlElement("Monster")]
    public List<Monster> monsters = new List<Monster>();
}

[Serializable]
public class Monster
{
    [XmlElement("MonsterID")]
    public string MonsterID;
    [XmlElement("Name")]
    public string Name;
    [XmlElement("Description")]
    public string Description;
    [XmlElement("Attack")]
    public int Attack;
    [XmlElement("AttackMul")]
    public float AttackMul;
    [XmlElement("MaxHP")]
    public int MaxHP;
    [XmlElement("MaxHPMul")]
    public float MaxHPMul;
    [XmlElement("AttackRange")]
    public int AttackRange;
    [XmlElement("AttackRangeMul")]
    public float AttackRangeMul;
    [XmlElement("AttackSpeed")]
    public float AttackSpeed;
    [XmlElement("MoveSpeed")]
    public float MoveSpeed;
    [XmlElement("MinExp")]
    public int MinExp;
    [XmlElement("MaxExp")]
    public int MaxExp;
    [XmlElement("DropItem")]
    public string DropItem;
}
