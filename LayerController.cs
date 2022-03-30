using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class LayerController : MonoBehaviour
{
    public GameObject selectedOn;
    public GameObject selectedOff;
    public GameObject deleteButton;

    internal bool isSelected;

    private ButtonConfigHelper buttonConfigHelper;
    private LayersManager layersManager;

    private GameObject drawingLayer;
    [SerializeField] private GameObject DisplayObjectBlock;

    // Start is called before the first frame update
    void Start()
    {
        buttonConfigHelper = this.gameObject.GetComponent<ButtonConfigHelper>();
        layersManager = this.gameObject.transform.root.GetComponent<LayersManager>();
        DisplayObjectBlock = GameObject.Find(this.gameObject.name + "/DisplayBlock/ObjectBlock");
        if (this.gameObject.name == "Layer1")
        {
            selectedOn.SetActive(true);
            selectedOff.SetActive(false);
        }
        else
        {
            selectedOn.SetActive(false);
            selectedOff.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(layersManager == null || DisplayObjectBlock == null)
        {
            layersManager = this.gameObject.transform.root.GetComponent<LayersManager>();
            DisplayObjectBlock = GameObject.Find(this.gameObject.name + "/DisplayBlock/ObjectBlock");
        }
        if (drawingLayer == null)
        {
            drawingLayer = layersManager.layersInDrawing[0];
        }
        else
        {
            drawingLayer = layersManager.selectedDrawingLayer;
        }
        buttonConfigHelper.MainLabelText = this.gameObject.name;
        if (drawingLayer.name == this.gameObject.name && drawingLayer.tag == "LayerDrawing" && DisplayObjectBlock != null)
        {
            ChangeColorInLayer();
        }
    }

    public void selectLayer()
    {
        layersManager.selectOneLayer(this.gameObject.name);
    }

    public void deletLayer()
    {
        layersManager.deleteOneLayer(this.gameObject.name);
    }

    internal void layerSelected()
    {
        if (isSelected)
        {
            selectedOn.SetActive(true);
            selectedOff.SetActive(false);
        }
        else
        {
            selectedOn.SetActive(false);
            selectedOff.SetActive(true);
        }
    }

    void ChangeColorInLayer()
    {
        Material layerColor = DisplayObjectBlock.GetComponent<Renderer>().sharedMaterial;
        if (drawingLayer.transform.childCount > 0)
        {
            GameObject[] GetLayerInfo = new GameObject[drawingLayer.transform.childCount];
            GameObject LineLayer = null;
            GameObject ObjectLayer = null;
            for(int i = 0;i < GetLayerInfo.Length; i++)
            {
                GetLayerInfo[i] = drawingLayer.transform.GetChild(i).gameObject;
                if (GetLayerInfo[i].name == "Line1")
                {
                    LineLayer = GetLayerInfo[i];
                }
                if (GetLayerInfo[i].name == "Object1")
                {
                    ObjectLayer = GetLayerInfo[i];
                }
            }

            if(LineLayer != null)
            {
                Transform[] layerLines = LineLayer.GetComponentsInChildren<Transform>();
                for (int j = 0; j < layerLines.Length; j++)
                {
                    //layerLines[i] = LineLayer.transform.GetChild(j);
                    if (layerLines[j].TryGetComponent<LineRenderer>(out var lineRenderer))
                    {
                        LineRenderer setLineColor = lineRenderer;
                        setLineColor.material = layerColor;
                        //setLineColor.material.SetColor("_Color", layerColor.color);
                    }
                    //else
                    //{
                    //    LineRenderer setLineColor = layerLines[i].GetComponent<LineRenderer>();
                    //    setLineColor.sharedMaterial = lineColor;
                    //    //setLineColor.material.SetColor("_Color", lineColor.color);
                    //}
                }
            }

            if(ObjectLayer != null)
            {
                Transform[] layerObjects = ObjectLayer.GetComponentsInChildren<Transform>();
                for (int k = 0; k < layerObjects.Length; k++)
                {
                    //layerLines[k] = LineLayer.transform.GetChild(k);
                    if (layerObjects[k].TryGetComponent<Renderer>(out var renderer))
                    {
                        Renderer setObjectColor = renderer;
                        setObjectColor.material.color = layerColor.color;
                        //setObjectColor.material.SetColor("_Color", layerColor.color);
                    }
                    //else
                    //{
                    //    LineRenderer setLineColor = layerLines[i].GetComponent<LineRenderer>();
                    //    setLineColor.sharedMaterial = lineColor;
                    //    //setLineColor.material.SetColor("_Color", lineColor.color);
                    //}
                }
            }
        }
    }
}
