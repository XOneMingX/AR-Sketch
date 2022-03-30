using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ListLayer
{
    public string name;
    public Vector3 position;

    public ListLayer(GameObject listLayer)
    {
        name = listLayer.name;
        position = listLayer.transform.position;
    }

    public void layerBuild()
    {
        GameObject listLayerCollection = GameObject.Find("LayersManager/LayersList/ScrollParent/ScrollingObjectCollection/Container/LayersListCollection");
        Quaternion newLayerRotation = GameObject.Find("LayersManager/LayersList/ScrollParent").transform.rotation;
        GameObject newLayer = GameObject.Instantiate(LayersManager.getLayerPrefab);
        newLayer.name = name;
        newLayer.transform.position = position;
        newLayer.transform.rotation = newLayerRotation;
        newLayer.transform.parent = listLayerCollection.transform;
    }
}
