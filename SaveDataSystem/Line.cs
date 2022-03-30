using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//use system namespace to make this class serializable
[Serializable]
public class Line
{
    public string name;
    public string tag;
    public Vector3[] positions;
    public Gradient color;
    public AnimationCurve width;

    //Line class Initializer. Takes in a LineRenderer and stores its data in the respective places
    public Line(LineRenderer lineRenderer)
    {
        positions = new Vector3[lineRenderer.positionCount];
        lineRenderer.GetPositions(positions);
        name = lineRenderer.name;
        tag = lineRenderer.tag;
        color = lineRenderer.colorGradient;
        width = lineRenderer.widthCurve;
    }   

    public void Drawing(GameObject AttributedLayer)
    {
        GameObject parentLayer = AttributedLayer;
        GameObject newLineRenderer = GameObject.Instantiate(Draw.staticLinePrefab, Vector3.zero, Quaternion.identity);
        newLineRenderer.transform.parent = parentLayer.transform;
        LineRenderer lineRenderer = newLineRenderer.GetComponent<LineRenderer>();
        lineRenderer.positionCount = positions.Length;
        lineRenderer.SetPositions(positions);
        lineRenderer.name = name;
        lineRenderer.tag = tag;
        lineRenderer.colorGradient = color;
        lineRenderer.widthCurve = width;
    }
}
