using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSave : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
       Debug.Log(this.gameObject.transform.localPosition);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        // To save
        ES3AutoSaveMgr.Current.settings.path = Application.dataPath + "/Resources/SaveSystem/TestSave.dat";
        ES3AutoSaveMgr.Current.Save();
    }

    public void Load()
    {
        // To load
        ES3AutoSaveMgr.Current.settings.path = Application.dataPath + "/Resources/SaveSystem/TestSave.dat";
        ES3AutoSaveMgr.Current.Load();
    }
}
