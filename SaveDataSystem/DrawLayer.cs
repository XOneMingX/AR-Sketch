using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class DrawLayer
{
    public string name;
    public Vector3 position;
    public List<Line> lines;
    public List<Generates> generates;

    public DrawLayer(GameObject drawLayer)
    {
        lines = new List<Line>();
        generates = new List<Generates>();
        name = drawLayer.name;
        position = drawLayer.transform.position;
        //Save lines & generate
        GameObject[] GetLayerInfo = new GameObject[drawLayer.transform.childCount];
        GameObject LineLayer = null;
        GameObject ObjectLayer = null;
        if (GetLayerInfo.Length > 0)
        {
            for (int i = 0; i < GetLayerInfo.Length; i++)
            {
                GetLayerInfo[i] = drawLayer.transform.GetChild(i).gameObject;
                if (GetLayerInfo[i].name == "Line1")
                {
                    LineLayer = GetLayerInfo[i];
                    LineRenderer getLine = GetLayerInfo[i].GetComponent<LineRenderer>();
                    lines.Add(new Line(getLine));
                }
                if (GetLayerInfo[i].name == "Object1")
                {
                    ObjectLayer = GetLayerInfo[i];
                    generates.Add(new Generates(GetLayerInfo[i]));
                }
            }

            if (LineLayer != null && LineLayer.transform.childCount > 0)
            {
                Transform[] GetAllLines = LineLayer.GetComponentsInChildren<Transform>();
                for (int j = 0; j < GetAllLines.Length; j++)
                {
                    if(GetAllLines[j].name != "Line1")
                    {
                        LineRenderer getLine = GetAllLines[j].GetComponent<LineRenderer>();
                        lines.Add(new Line(getLine));
                    }
                }
            }
            if (ObjectLayer != null && ObjectLayer.transform.childCount > 0)
            {
                Transform[] GetAllObjects = ObjectLayer.GetComponentsInChildren<Transform>();
                for (int k = 0; k < GetAllObjects.Length; k++)
                {
                    if(GetAllObjects[k].name != "Object1")
                    {
                        generates.Add(new Generates(GetAllObjects[k].gameObject));
                    }
                }
            }
        }
    }

    public void layerBuild()
    {
        GameObject drawLayerCollection = GameObject.Find("LayersManager/DrawingLayersCollection");
        GameObject newLayer = new GameObject();
        newLayer.name = name;
        newLayer.transform.position = position;
        newLayer.transform.parent = drawLayerCollection.transform;
        foreach (Line line in lines)
        {
            line.Drawing(newLayer);
        }
        foreach (Generates objects in generates)
        {
            objects.Generating(newLayer);
        }
    }
}
