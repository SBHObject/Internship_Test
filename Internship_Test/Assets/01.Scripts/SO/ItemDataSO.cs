using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
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

[Serializable]
[XmlRoot("ItemData")]
public class ItemData
{
    [XmlElement("Item")]
    public List<Item> items = new List<Item>();
}

[Serializable]
public class Item
{
    [XmlElement("ItemID")]
    public int ItemID;
    [XmlElement("Name")]
    public string Name;
    [XmlElement("Description")]
    public string Description;
    [XmlElement ("UnlockLevel")]
    public int UnlockLevel;

    [XmlElement("MaxHP")]
    public int MaxHP;
    [XmlElement("MaxHpMul")]
    public float MaxHpMul;
    [XmlElement("MaxMp")]
    public int MaxMp;
    [XmlElement("MaxMpMul")]
    public float MaxMpMul;

    [XmlElement("MaxAtk")]
    public int MaxAtk;
    [XmlElement("MaxAtkMul")]
    public float MaxAtkMul;
    [XmlElement("AtkSpeed")]
    public float AtkSpeed;
    [XmlElement("AtkRange")]
    public float AtkRange;

    [XmlElement("Exp")]
    public int Exp;
    [XmlElement("ExpMul")]
    public float ExpMul;

    [XmlElement("Status")]
    public int Status;
}
