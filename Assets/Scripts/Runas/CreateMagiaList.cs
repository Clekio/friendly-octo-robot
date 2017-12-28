using UnityEngine;
using System.Collections;
using UnityEditor;

public class CreateMagiaList
{
    [MenuItem("Assets/Create/Magia List")]
    public static MagiaList Create()
    {
        MagiaList asset = ScriptableObject.CreateInstance<MagiaList>();

        AssetDatabase.CreateAsset(asset, "Assets/Scripts/Runas/ListasDeMagias/InventoryItemList.asset");
        AssetDatabase.SaveAssets();
        return asset;
    }
}
