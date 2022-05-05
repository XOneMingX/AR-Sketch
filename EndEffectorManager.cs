using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using Microsoft.MixedReality.Toolkit.UI;

public class EndEffectorManager : MonoBehaviour
{
    public GameObject roboticGripper;
    private GameObject endEffector;
    public Transform leftFinger;
    public Transform rightFinger;

    private Vector3 indexTip;
    private Vector3 Palm;
    private Quaternion R_indexTip;
    private Quaternion R_Palm;
    private MixedRealityPose pose;
    // Start is called before the first frame update
    void Start()
    {
        endEffector = new GameObject();
        Vector3 midFinger = (leftFinger.position + rightFinger.position) / 2;
        endEffector.transform.position = new Vector3(midFinger.x, midFinger.y, midFinger.z);
        endEffector.name = "EndEffector";
        roboticGripper.transform.parent = endEffector.transform;
    }

    // Update is called once per frame
    void Update()
    {
        trackFinger();
    }

    private void trackFinger()
    {
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Both, out pose))
        {
            indexTip = pose.Position;
            R_indexTip = pose.Rotation;
        }
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.Palm, Handedness.Both, out pose))
        {
            Palm = pose.Position;
            R_Palm = pose.Rotation;
        }
        endEffector.transform.position = indexTip;
        Vector3 eulerRotation = new Vector3(R_Palm.eulerAngles.x, R_Palm.eulerAngles.y - 180f, 0);
        endEffector.transform.rotation = Quaternion.Euler(eulerRotation);
    }


}
