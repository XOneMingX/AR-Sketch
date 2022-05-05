using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;

public class Draw : MonoBehaviour
{
    private IMixedRealityHandJointService handJointService;
    private IMixedRealityHandJointService HandJointService =>
        handJointService ??
        (handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>());
    private MixedRealityPose? previousLeftHandPose;
    private MixedRealityPose? previousRightHandPose;
    private const float GrabThreshold = 0.02f;
    private MixedRealityPose pose;

    private Vector3 indexTip;
    private Vector3 thumbTip;

    //public Camera m_camera;
    public GameObject brush;
    public static GameObject staticLinePrefab;

    private GameObject selectedDrawingLayer;
    LineRenderer currentLineRenderer;
    Vector3 lastPos;
    bool isStartDraw;

    private LayersManager layersManager;

    void Start()
    {
        layersManager = GameObject.Find("LayersManager").GetComponent<LayersManager>();
        staticLinePrefab = brush;
    }
    void Update()
    {
        if(layersManager == null)
        {
            layersManager = GameObject.Find("LayersManager").GetComponent<LayersManager>();
        }
        if(HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip,Handedness.Both,out pose))
        {
            indexTip = pose.Position;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.ThumbTip, Handedness.Both, out pose))
        {
            thumbTip = pose.Position;
        }
        Drawing();
        selectedDrawingLayer = layersManager.selectedDrawingLayer;
    }

    void Drawing()
    {
        float StartDrawing = Mathf.Abs(Vector3.Distance(indexTip, thumbTip));
        if(StartDrawing != 0)
        {
            if (StartDrawing > GrabThreshold)
            {
                isStartDraw = false;
                currentLineRenderer = null;
            }
            if (StartDrawing < GrabThreshold && !isStartDraw)
            {
                CreateBrush();
                isStartDraw = true;
            }
            else if (StartDrawing < GrabThreshold && isStartDraw)
            {
               //CreateBrush();
               PointToMousePos();
            }

        }
    }

    void CreateBrush()
    {
        GameObject brushInstance = Instantiate(brush, new Vector3(0, 0, 0), Quaternion.identity);
        
        brushInstance.tag = "Line";
        GameObject[] GetLayerInfo = new GameObject[selectedDrawingLayer.transform.childCount];
        GameObject LineLayer = null;
        for (int i = 0; i < GetLayerInfo.Length; i++)
        {
            GetLayerInfo[i] = selectedDrawingLayer.transform.GetChild(i).gameObject;
            if (GetLayerInfo[i].name == "Line1")
            {
                LineLayer = GetLayerInfo[i];
            }
        }
        if (selectedDrawingLayer.transform.childCount == 0 || LineLayer == null)
        {
            brushInstance.transform.parent = selectedDrawingLayer.transform;
            brushInstance.name = "Line1";
        }
        else
        {
            Transform[] GetAllLine = LineLayer.GetComponentsInChildren<Transform>();
            for (int i = 0; i < GetAllLine.Length; i++)
            {
                brushInstance.transform.parent = GetAllLine[GetAllLine.Length - 1].transform;
                brushInstance.name = "Line" + (GetAllLine.Length + 1);
            }
        }
        
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        currentLineRenderer.SetPosition(0, indexTip);
        currentLineRenderer.SetPosition(1, indexTip);
    }

    void AddAPoint(Vector3 pointPos)
    {
        //currentLineRenderer.positionCount = 0;
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos()
    {
        Vector3 _figurePos = indexTip;//(new Vector3(Input.mousePosition.x,Input.mousePosition.y, playerWordDir.z));
        Vector3 figurePos = new Vector3(_figurePos.x, _figurePos.y, _figurePos.z);
        if (lastPos != figurePos)
        {
            AddAPoint(figurePos);
            lastPos = figurePos;
        }
    }

    public void ClearLine()
    {
        GameObject[] DrewLines = new GameObject[selectedDrawingLayer.transform.childCount];
        for (int i = 0; i < selectedDrawingLayer.transform.childCount; i++)
        {
            DrewLines[i] = selectedDrawingLayer.transform.GetChild(i).gameObject;
            Destroy(DrewLines[i]);
        }
    }


}