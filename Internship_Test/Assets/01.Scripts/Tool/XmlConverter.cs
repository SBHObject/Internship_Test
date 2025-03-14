using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public enum EDataType
{
    Item,
    Monster
}

public class XmlConverter : EditorWindow
{
    [MenuItem("Tool/Xml To SO Converter")]
    private static void ConverteItemData()
    {
        EditorWindow wnd = GetWindow<XmlConverter>();
        wnd.titleContent = new GUIContent("Xml to SO Converter");   
    }

    private void OnGUI()
    {
        if(GUI.Button(new Rect(0,0,300,30), "Converte Monster data"))
        {
            var path = EditorUtility.OpenFilePanel("Overwrite with Xml", "", "xml");
            if(path.Length != 0)
            {
                Load(path, EDataType.Monster);
            }
        }

        if (GUI.Button(new Rect(0, 30, 300, 30), "Converte Item data"))
        {
            var path = EditorUtility.OpenFilePanel("Overwrite with Xml", "", "xml");
            if (path.Length != 0)
            {
                Load(path, EDataType.Item);
            }
        }
    }

    private void Load(string path, EDataType type)
    {
        if(!File.Exists(path))
        {
            Debug.Log("파일을 찾을 수 없습니다.");
            return;
        }

        XmlSerializer serializer;
        switch (type)
        {
            case EDataType.Item:
                serializer = new XmlSerializer(typeof(ItemData));

                using (StreamReader reader = new StreamReader(path))
                {
                    ItemData data = (ItemData)serializer.Deserialize(reader);

                    CreateSO(data);
                }

                break;
            case EDataType.Monster:
                serializer = new XmlSerializer(typeof(MonsterData));

                using (StreamReader reader = new StreamReader(path))
                {
                    MonsterData data = (MonsterData)serializer.Deserialize(reader);

                    CreateSO(data);
                }
                break;
            default:
                Debug.Log("잘못된 요청입니다.");
                break;
        }
    }

    private void CreateSO(ItemData data)
    {
        foreach(var item in data.items)
        {
            ItemDataSO itemSO = ScriptableObject.CreateInstance<ItemDataSO>();

            itemSO.ItemID = item.ItemID;
            itemSO.Name = item.Name;
            itemSO.Description = item.Description;
            itemSO.UnlockLevel = item.UnlockLevel;
            itemSO.MaxHP = item.MaxHP;
            itemSO.MaxHpMul = item.MaxHpMul;
            itemSO.MaxMp = item.MaxMp;
            itemSO.MaxMpMul = item.MaxMpMul;
            itemSO.MaxAtk = item.MaxAtk;
            itemSO.MaxAtkMul = item.MaxAtkMul;
            itemSO.AtkSpeed = item.AtkSpeed;
            itemSO.AtkRange = item.AtkRange;
            itemSO.Exp = item.Exp;
            itemSO.ExpMul = item.ExpMul;
            itemSO.Status = item.Status;

            string savePath = $"Assets/Resources/Data/Item/{item.ItemID}.asset";
            AssetDatabase.CreateAsset(itemSO, savePath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private void CreateSO(MonsterData data)
    {
        foreach(var item in data.monsters)
        {
            MonsterDataSO monsterSO = ScriptableObject.CreateInstance<MonsterDataSO>();

            monsterSO.MonsterID = item.MonsterID;
            monsterSO.Name = item.Name;
            monsterSO.Description = item.Description;
            monsterSO.Attack = item.Attack;
            monsterSO.AttackMul = item.AttackMul;
            monsterSO.MaxHP = item.MaxHP;
            monsterSO.MaxHPMul = item.MaxHPMul;
            monsterSO.AttackRange = item.AttackRange;
            monsterSO.AttackRangeMul = item.AttackRangeMul;
            monsterSO.AttackSpeed = item.AttackSpeed;
            monsterSO.MoveSpeed = item.MoveSpeed;
            monsterSO.MinExp = item.MinExp;
            monsterSO.MaxExp = item.MaxExp;

            monsterSO.DropItem = new List<int>();
            string[] split = item.DropItem.Split();

            for(int i = 0; i < split.Length; i++)
            {
                monsterSO.DropItem.Add(int.Parse(split[i]));
            }

            string savePath = $"Assets/Resources/Data/Monster/{item.MonsterID}.asset";
            AssetDatabase.CreateAsset(monsterSO, savePath);
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
