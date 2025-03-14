using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public abstract class XmlToSOBase : ScriptableObject
{
    public void Save(string path)
    {
        AssetDatabase.CreateAsset(this, path);
        EditorUtility.SetDirty(this);
        AssetDatabase.SaveAssets();

        AssetDatabase.Refresh();
    }
}
