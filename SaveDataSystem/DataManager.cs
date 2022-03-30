using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    [SerializeField]
    public List<ListLayer> listLayer;
    public List<DrawLayer> drawLayer;

    //DataManager class Initializer which takes in a Listlayer & DrawLayer List and stores it as the both class list.
    public DataManager(List<GameObject> listLayers, List<GameObject> drawLayers)
    {
        listLayer = new List<ListLayer>();
        drawLayer = new List<DrawLayer>();

        foreach (GameObject getListLayer in listLayers)
        {
            listLayer.Add(new ListLayer(getListLayer));
        }
        foreach (GameObject getDrawLayer in drawLayers)
        {
            drawLayer.Add(new DrawLayer(getDrawLayer));
        }

    }

    //Load the saved data of list layer and draw layer
    public void buildListLayer()
    {
        foreach (ListLayer layers in listLayer)
        {
            layers.layerBuild();
        }
    }

    public void buildDrawLayer()
    {
        foreach (DrawLayer layers in drawLayer)
        {
            layers.layerBuild();
        }
    }
}
