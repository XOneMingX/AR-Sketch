using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//allows the use of File class
using System.IO;
using System;

public class FileManager : MonoBehaviour
{
    bool isStore = false;
    public void Save()
    {
        if (LayersManager.listLayers.Count > 0 && LayersManager.drawLayers.Count > 0)
        {
            string filePath = Application.dataPath + "/Resources/SaveSystem/SaveData.dat";
            DataManager dataSave = new DataManager(LayersManager.listLayers, LayersManager.drawLayers);
            if (WriteToFile(filePath, JsonUtility.ToJson(dataSave)))
            {
                Debug.Log("Save successful");
            }
            //string jsonString = JsonUtility.ToJson(dataSave);
            //File.WriteAllText(filePath, jsonString);
        }
    }

    public static bool Load()
    {
        string filePath = Application.dataPath + "/Resources/SaveSystem/SaveData.dat";

        try
        {
            string jsonString = File.ReadAllText(filePath);
            DataManager dataLoad = JsonUtility.FromJson<DataManager>(jsonString);
            dataLoad.buildListLayer();
            dataLoad.buildDrawLayer();
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {filePath} with exception {e}");
            return false;
        }
        /*
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            DataManager dataLoad = JsonUtility.FromJson<DataManager>(jsonString);
            dataLoad.buildListLayer();
            dataLoad.buildDrawLayer();
            return true;
        }
        else
        {
            Debug.Log("No savefile with that name");
            return false;
        }
        */
    }

    static bool WriteToFile(string filePath, string fileContents)
    {
        try
        {
            File.WriteAllText(filePath, fileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {filePath} with exception {e}");
            return false;
        }
    }
}