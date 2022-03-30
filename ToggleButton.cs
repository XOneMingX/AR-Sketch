using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleButton : MonoBehaviour
{
    public void Toggle()
    {
        if(this.gameObject.activeSelf == this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(!this.gameObject.activeSelf);
        }

    }
}
