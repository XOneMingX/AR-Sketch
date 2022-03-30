using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Microsoft.MixedReality.Toolkit.UI;

public class ObjectSelect : MonoBehaviour
{
    private ButtonConfigHelper buttonConfigHelper;
    private UnityEvent ObjectSelection;
    private ObjectCreate objectCreate;
    private GameObject selfObject;
    // Start is called before the first frame update
    void Start()
    {
        buttonConfigHelper = this.gameObject.GetComponent<ButtonConfigHelper>();
        ObjectSelection = new UnityEvent();
        ObjectSelection = buttonConfigHelper.OnClick;
        ObjectSelection.AddListener(selectObject);
    }

    void Update()
    {
        if (selfObject == null)
        {
            selfObject = Resources.Load<GameObject>("Prefabs/" + this.gameObject.name);
        }
    }

    void selectObject()
    {
        GameObject Draw = GameObject.Find("Draw");
        objectCreate = Draw.GetComponent<ObjectCreate>();
        objectCreate.SelectObject(selfObject);
    }
}
