using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;

public class LayersManager : MonoBehaviour
{
    public GameObject layerPrefab;
    public static GameObject getLayerPrefab;

    public GameObject layersListCollection;
    private GameObject[] layersInList;
    internal Vector3[] layersTransformInList;

    public GameObject layersDrawingCollection;
    internal GameObject[] layersInDrawing;
    internal Vector3[] layersTransformInDrawing;

    [SerializeField] internal GameObject selectedListLayer;
    [SerializeField] internal GameObject selectedDrawingLayer;

    private GridObjectCollection gridObjectCollection;
    private ScrollingObjectCollection scrollingObjectCollection;

    bool isReset;
    bool isSort;

    public GameObject ColorBlock;
    internal GameObject DisplayObjectBlock;

    //Collect Data which need to be saved
    public static List<GameObject> listLayers = new List<GameObject>();
    public static List<GameObject> drawLayers = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        getLayerPrefab = layerPrefab;
        gridObjectCollection = layersListCollection.GetComponent<GridObjectCollection>();
        scrollingObjectCollection = layersListCollection.transform.parent.transform.parent.GetComponent<ScrollingObjectCollection>();
        layersList();
    }

    // Update is called once per frame
    void Update()
    {
        if (isReset)
        {
            StartCoroutine(waitForReset());
        }
        if (layersInList == null || layersInDrawing == null)
        {
            layersList();
        }
        if (isSort)
        {
            SortDrawLayerItems();
        }
        if (selectedListLayer == null)
        {
            selectedListLayer = layersInList[0];
        }
        if (selectedDrawingLayer == null)
        {
            selectedDrawingLayer = layersInDrawing[0];
        }
        //foreach(GameObject layer in listLayers)
        //{
        //    Debug.Log(layer);
        //}
    }

    void layersList()
    {
        //Debug.Log(layersListCollection.transform.childCount);
        layersInList = new GameObject[layersListCollection.transform.childCount];
        layersTransformInList = new Vector3[layersListCollection.transform.childCount];
        for (int i = 0; i < layersListCollection.transform.childCount; i++)
        {
            layersInList[i] = layersListCollection.transform.GetChild(i).gameObject;
            layersTransformInList[i] = layersListCollection.transform.GetChild(i).position;
            layersInList[i].name = "Layer" + (i + 1);

            //Get List Layer Data
            if (!listLayers.Contains(layersInList[i]))
            {
                listLayers.Add(layersInList[i]);
            }
        }

        layersInDrawing = new GameObject[layersDrawingCollection.transform.childCount];
        layersTransformInDrawing = new Vector3[layersDrawingCollection.transform.childCount];
        for (int i = 0; i < layersDrawingCollection.transform.childCount; i++)
        {
            layersInDrawing[i] = layersDrawingCollection.transform.GetChild(i).gameObject;
            layersTransformInDrawing[i] = layersDrawingCollection.transform.GetChild(i).position;
            layersInDrawing[i].name = "Layer" + (i + 1);

            //Get Draw Layer Data
            if (!drawLayers.Contains(layersInDrawing[i]))
            {
                drawLayers.Add(layersInDrawing[i]);
            }
        }

    }

    internal void selectOneLayer(string layerName)
    {
        foreach(GameObject layer in layersInList)
        {
            if(layer.name == layerName)
            {
                selectedListLayer = layer;
                if (layer.TryGetComponent<LayerController>(out var layerController))
                {
                    layerController.isSelected = true;
                    layerController.layerSelected();
                }
            }
            else
            {
                if (layer.TryGetComponent<LayerController>(out var layerController))
                {
                    layerController.isSelected = false;
                    layerController.layerSelected();
                    layerController.deleteButton.SetActive(false);
                }
            }
        }
        foreach(GameObject drawyingLayer in layersInDrawing)
        {
            if (drawyingLayer.name == layerName)
            {
                selectedDrawingLayer = drawyingLayer;
            }
        }
    }

    public void AddLayer()
    {
        Quaternion newLayerRotation = this.gameObject.transform.GetChild(0).GetChild(0).transform.rotation;
        GameObject layerInList = Instantiate(layerPrefab, transform.position, newLayerRotation);
        layerInList.tag = "LayerList";
        layerInList.transform.parent = layersListCollection.transform;

        GameObject layerInDrawing = new GameObject();
        layerInDrawing.transform.parent = layersDrawingCollection.transform;
        layerInDrawing.tag = "LayerDrawing";

        isReset = true;
        //layersList();
    }

    internal void deleteOneLayer(string layerName)
    {
        foreach (GameObject layer in layersInList)
        {
            if(layersListCollection.transform.childCount > 1)
            {
                if (layer.name == layerName)
                {
                    listLayers.Remove(layer);
                    Destroy(layer);
                }
            }
        }

        foreach (GameObject layer in layersInDrawing)
        {
            if (layersDrawingCollection.transform.childCount > 1)
            {
                if (layer.name == layerName)
                {
                    drawLayers.Remove(layer);
                    Destroy(layer);
                }
            }
        }
        isReset = true;
    }

    public void ChangeLayerColor()
    {
        Material GetSelectedColor = ColorBlock.GetComponent<Renderer>().sharedMaterial;
        DisplayObjectBlock = GameObject.Find(selectedListLayer.name + "/DisplayBlock/ObjectBlock");
        Material LayerColor = DisplayObjectBlock.GetComponent<Renderer>().sharedMaterial;
        LayerColor.color = GetSelectedColor.color;
        //GameObject[] layerLines = new GameObject[selectedLayer.transform.childCount];
        //if (selectedLayer.transform.childCount > 0)
        //{
        //    for (int i = 0; i < layerLines.Length; i++)
        //    {
        //        layerLines[i] = selectedLayer.transform.GetChild(i).gameObject;
        //        LineRenderer setLineColor = layerLines[i].GetComponent<LineRenderer>();
        //        setLineColor.sharedMaterial = lineColor;
        //        //setLineColor.material.SetColor("_Color", lineColor.color);
        //    }
        //}
    }

    IEnumerator waitForReset()
    {
        listLayers.RemoveAll(item => item == null);
        drawLayers.RemoveAll(item => item == null);
        layersList();
        yield return new WaitForSeconds(0.1f);
        gridObjectCollection.UpdateCollection();
        scrollingObjectCollection.Reset();
        yield return new WaitForSeconds(0.1f);
        foreach (GameObject layer in layersInList)
        {
            if (layer.name == selectedListLayer.name && layer.tag == "LayerList")
            {
                if (layer.TryGetComponent<LayerController>(out var layerController))
                {
                    layerController.isSelected = true;
                    layerController.layerSelected();
                }
            }
            else
            {
                if (layer.TryGetComponent<LayerController>(out var layerController))
                {
                    layerController.isSelected = false;
                    layerController.layerSelected();
                    layerController.deleteButton.SetActive(false);
                }
            }
        }
        foreach (GameObject layer in layersInDrawing)
        {
            if (layer.name == selectedDrawingLayer.name && layer.tag == "LayerDrawing")
            {
                selectedDrawingLayer = layer;
            }
        }
        yield return new WaitForSeconds(0.5f);
        isReset = false;
        
    }

    #region Save&Load Layer Manage
    public void resetLayerSystemByLoad()
    {
        listLayers.Clear();
        drawLayers.Clear();
        foreach (GameObject oldListLayer in layersInList)
        {
            Destroy(oldListLayer);
        }
        foreach (GameObject oldDrawLayer in layersInDrawing)
        {
            Destroy(oldDrawLayer);
        }
        if (FileManager.Load())
        {
            selectedListLayer = layersInList[0];
            selectedDrawingLayer = layersInDrawing[0];
            isReset = true;
            isSort = true;
        }
    }

    void SortDrawLayerItems()
    {
        GameObject[] GetAllDrawLayers = new GameObject[layersDrawingCollection.transform.childCount];

        for (int i = 0; i < GetAllDrawLayers.Length; i++)
        {
            GetAllDrawLayers[i] = layersDrawingCollection.transform.GetChild(i).gameObject;
            GameObject[] GetLayerInfo = new GameObject[GetAllDrawLayers[i].transform.childCount];
            List<GameObject> LineCollection = new List<GameObject>();
            List<GameObject> ObjectCollection = new List<GameObject>();
            for (int k = 0; k < GetLayerInfo.Length; k++)
            {
                GetLayerInfo[k] = GetAllDrawLayers[i].transform.GetChild(k).gameObject;
                if (GetLayerInfo[k].tag == "Line")
                {
                    LineCollection.Add(GetLayerInfo[k]);
                }
                if (GetLayerInfo[k].tag == "Cube" || GetLayerInfo[k].tag == "Sphere" || GetLayerInfo[k].tag == "Cylinder" || GetLayerInfo[k].tag == "Capsule")
                {
                    ObjectCollection.Add(GetLayerInfo[k]);
                }
            }
            if (LineCollection.Count > 0)
            {
                for (int l = 0; l < LineCollection.Count; l++)
                {
                    if (l + 1 < LineCollection.Count)
                    {
                        LineCollection[l + 1].transform.parent = LineCollection[l].transform;
                    }

                }
            }
            if (ObjectCollection.Count > 0)
            {
                for (int o = 0; o < ObjectCollection.Count; o++)
                {
                    if (o + 1 < ObjectCollection.Count)
                    {
                        ObjectCollection[o + 1].transform.parent = ObjectCollection[o].transform;
                        ObjectCollection[o + 1].transform.localScale = new Vector3(1, 1, 1);
                    }

                }
            }
        }

        isSort = false;
    }
    #endregion
}
