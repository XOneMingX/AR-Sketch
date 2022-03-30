using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveManager : MonoBehaviour
{
    public GameObject SaveLayers;

    public void Save()
    {
        GameObject FindAnySavedLayer = Resources.Load<GameObject>("SaveSystem/LayersManager");
        if(FindAnySavedLayer == null)
        {
            string localPath = "Assets/Resources/SaveSystem/" + SaveLayers.name + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            PrefabUtility.SaveAsPrefabAsset(SaveLayers, localPath, out bool success);
        }
        else
        {
            string[] unusedFolder = { "Assets/Resources/SaveSystem" };
            foreach (var asset in AssetDatabase.FindAssets("", unusedFolder))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                AssetDatabase.DeleteAsset(path);
            }
            string localPath = "Assets/Resources/SaveSystem/" + SaveLayers.name + ".prefab";
            localPath = AssetDatabase.GenerateUniqueAssetPath(localPath);
            PrefabUtility.SaveAsPrefabAsset(SaveLayers, localPath, out bool success);
        }

    }

    public void Load()
    {
        GameObject GetSavedLayer = Resources.Load<GameObject>("SaveSystem/LayersManager");
        //GameObject GetSavedLayer =(GameObject)AssetDatabase.LoadAssetAtPath("Assets/SaveSystem/LayersManager.prefab", typeof(GameObject));
        GameObject SceneLayer = GameObject.Find("LayersManager");
        Destroy(SceneLayer);
        GameObject LoadedLayer = Instantiate(GetSavedLayer);
        LoadedLayer.name = "LayersManager";
        SaveLayers = LoadedLayer;

        //SaveLayers = PrefabUtility.InstantiatePrefab(LoadLayers) as GameObject;
    }

    static bool ValidateCreatePrefab()
    {
        return Selection.activeGameObject != null && !EditorUtility.IsPersistent(Selection.activeGameObject);
    }
}
