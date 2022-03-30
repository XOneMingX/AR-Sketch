using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//allows the use of File class
using System.IO;
using System;

public class TestFileManager : MonoBehaviour
{
    //bool isStore = false;
    //public void TestSave()
    //{
    //    if (LayersManager.listLayers.Count > 0 && LayersManager.drawLayers.Count > 0)
    //    {
    //        string filePath = Application.dataPath + "/Resources/SaveSystem/TestSaveData.dat";
    //        TestFileSave newSaveFile = new TestFileSave(LayersManager.listLayers, LayersManager.drawLayers);

    //        string jsonString = JsonUtility.ToJson(newSaveFile);
    //        File.WriteAllText(filePath, jsonString);
    //        isStore = true;
    //    }
    //    //ES3.Save("line", jsonString, filePath);
    //    if (isStore)
    //    {
    //        LayersManager.listLayers.Clear();
    //        LayersManager.drawLayers.Clear();
    //        isStore = false;
    //    }
    //}

    //public static bool TestLoad()
    //{
    //    string filePath = Application.dataPath + "/Resources/SaveSystem/TestSaveData.dat";


    //    if (File.Exists(filePath))
    //    {
    //        string jsonString = File.ReadAllText(filePath);
    //        //string jsonString = ES3.Load("line",filePath);
    //        TestFileSave newSaveFile = JsonUtility.FromJson<TestFileSave>(jsonString);
    //        //newSaveFile.Draw();
    //        //newSaveFile.ReGenerate();
    //        newSaveFile.buildListLayer();
    //        newSaveFile.buildDrawLayer();
    //        return true;
    //    }
    //    else
    //    {
    //        Debug.Log("No savefile with that name");
    //        return false;
    //    }
        /* Original Load Method
        if (File.Exists(filePath))
        {
            string jsonString = File.ReadAllText(filePath);
            SaveFile newSaveFile = JsonUtility.FromJson<SaveFile>(jsonString);
            newSaveFile.Draw();
        }
        else
        {
            Debug.Log("No savefile with that name");
        }
        */
    //}
}
