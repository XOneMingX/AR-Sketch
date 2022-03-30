using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LineControl : MonoBehaviour
{
    public GameObject Brush;

    public GameObject LineWidthControl;
    public TMP_InputField TMPLineWidth;
    private LineRenderer lineRenderer;
    private float oldLineWidth;
    private float newLineWidth;

    //public GameObject ColorBlock;
    //private Material lineColor;

    private LayersManager layersManager;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = Brush.GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.01f;

        newLineWidth = float.Parse(TMPLineWidth.text);
        oldLineWidth = newLineWidth;

        layersManager = GameObject.Find("LayersManager").GetComponent<LayersManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LineWidthControl.activeSelf)
        {
            AdjustLineWidth();
        }
    }

    //public void ChangeLineColorInLayer()
    //{
    //    lineColor = ColorBlock.GetComponent<Renderer>().material;
    //    GameObject selectedLayer = layersManager.selectedDrawingLayer;
    //    GameObject[] layerLines = new GameObject[selectedLayer.transform.childCount];
    //    if (selectedLayer.transform.childCount > 0)
    //    {
    //        for (int i = 0; i < layerLines.Length; i++)
    //        {
    //            layerLines[i] = selectedLayer.transform.GetChild(i).gameObject;
    //            LineRenderer setLineColor = layerLines[i].GetComponent<LineRenderer>();
    //            setLineColor.sharedMaterial = lineColor;
    //            //setLineColor.material.SetColor("_Color", lineColor.color);
    //        }
    //    }
    //}

    void AdjustLineWidth()
    {
        if (oldLineWidth != newLineWidth)
        {
            float getLineWidth = newLineWidth / 100;
            lineRenderer.startWidth = getLineWidth;
            TMPLineWidth.text = newLineWidth.ToString();
            oldLineWidth = newLineWidth;
        }
    }

    public void AdjustLineWidthByUpButton()
    {
        newLineWidth = float.Parse(TMPLineWidth.text);
        newLineWidth += 1;
    }

    public void AdjustLineWidthByDownButton()
    {
        newLineWidth = float.Parse(TMPLineWidth.text);
        if(newLineWidth > 1)
        {
            newLineWidth -= 1;
        }
    }
}
