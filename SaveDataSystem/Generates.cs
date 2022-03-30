using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class Generates
{
    public string name;
    public string tag;
    public Vector3 position;
    public Vector3 scale;
    //public int orderIndex;

    public Generates(GameObject generates)
    {
        name = generates.name;
        tag = generates.tag;
        position = generates.transform.position;
        scale = generates.transform.localScale;
        //string readName = Regex.Match(name, @"\d+").Value;
        //orderIndex = Int32.Parse(readName);
    }

    public void Generating(GameObject AttributedLayer)
    {
        GameObject parentLayer = AttributedLayer;
        PrimitiveType block = (PrimitiveType)System.Enum.Parse(typeof(PrimitiveType), tag);
        GameObject newGenerate = GameObject.CreatePrimitive(block);
        newGenerate.transform.parent = parentLayer.transform; 
        newGenerate.name = name;
        newGenerate.tag = tag;
        newGenerate.transform.position = position;
        newGenerate.transform.localScale = scale;
        //string readName = Regex.Match(newGenerate.name, @"\d+").Value;
        //orderIndex = Int32.Parse(readName);
    }
}
