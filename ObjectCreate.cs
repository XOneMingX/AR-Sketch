using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;


public class ObjectCreate : MonoBehaviour
{
    private IMixedRealityHandJointService handJointService;

    private IMixedRealityHandJointService HandJointService =>
        handJointService ??
        (handJointService = CoreServices.GetInputSystemDataProvider<IMixedRealityHandJointService>());

    private MixedRealityPose? previousLeftHandPose;

    private MixedRealityPose? previousRightHandPose;

    private const float GrabThreshold = 0.4f;

    private GameObject trackingObject;
    private Vector3 trackingTransform;
    private bool isCreated = false;

    public GameObject ObjectMenu;
    internal bool startCreateObject = false;
    private GameObject selectedObject;

    private LayersManager layersManager;
    private GameObject selectedDrawingLayer;

    // Start is called before the first frame update
    void Start()
    {
        if (trackingObject == null)
        {
            trackingObject = GameObject.Find("HandTrack");
        }
        else
        {
            Debug.Log("Object is missed!!");
        }
        layersManager = GameObject.Find("LayersManager").GetComponent<LayersManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ObjectMenu == null)
        {
            ObjectMenu = GameObject.Find("LayersManager/LayersList/ScrollParent/Features/ObjectsSelect");
        }
        if (startCreateObject)
        {
            if (layersManager == null)
            {
                layersManager = GameObject.Find("LayersManager").GetComponent<LayersManager>();
            }
            if (IsGrabbing(Handedness.Both) && selectedObject != null && !isCreated)
            {
                CreatePoint();
                isCreated = true;
            }
            if (!IsGrabbing(Handedness.Both))
            {
                isCreated = false;
            }
        }
        isStartCreate();

    }

    public static bool IsGrabbing(Handedness trackedHand)
    {
        return HandPoseUtils.IndexFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.MiddleFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.RingFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.PinkyFingerCurl(trackedHand) > GrabThreshold &&
               HandPoseUtils.ThumbFingerCurl(trackedHand) > GrabThreshold;
    }

    public void SelectObject(GameObject DifferentShapesObject)
    {
        selectedObject = DifferentShapesObject;
    }

    void isStartCreate()
    {
        if (ObjectMenu.activeSelf)
        {
            startCreateObject = ObjectMenu.activeSelf;
        }
        else
        {
            startCreateObject = false;
        }
    }

    void CreatePoint()
    {
        selectedDrawingLayer = layersManager.selectedDrawingLayer;
        trackingTransform = trackingObject.transform.position;
        GameObject CreatedObject = Instantiate(selectedObject);
        CreatedObject.tag = selectedObject.name;
        GameObject[] GetLayerInfo = new GameObject[selectedDrawingLayer.transform.childCount];
        GameObject ObjectLayer = null;
        for (int i = 0; i < GetLayerInfo.Length; i++)
        {
            GetLayerInfo[i] = selectedDrawingLayer.transform.GetChild(i).gameObject;
            if (GetLayerInfo[i].name == "Object1")
            {
                ObjectLayer = GetLayerInfo[i];
            }
        }
        if (selectedDrawingLayer.transform.childCount == 0 || ObjectLayer == null)
        {
            CreatedObject.transform.parent = selectedDrawingLayer.transform;
            CreatedObject.name = "Object1";
        }
        else
        {
            Transform[] GetAllLine = ObjectLayer.GetComponentsInChildren<Transform>();
            for (int i = 0; i < GetAllLine.Length; i++)
            {
                CreatedObject.transform.parent = GetAllLine[GetAllLine.Length - 1].transform;
                CreatedObject.name = "Object" + (GetAllLine.Length + 1);
            }
        }
        CreatedObject.transform.position = trackingTransform;
        Vector3 setScale = CreatedObject.transform.localScale;
        setScale = new Vector3(0.05f * setScale.x, 0.05f * setScale.y, 0.05f * setScale.z);
        CreatedObject.transform.localScale = setScale;
        CreatedObject.AddComponent<ObjectManipulator>();
        CreatedObject.AddComponent<NearInteractionGrabbable>();
        //sphere.AddComponent<TouchDetection>();
        CreatedObject.GetComponent<ObjectManipulator>().enabled = false;
    }
}
