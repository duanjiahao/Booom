using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenControl : MonoBehaviour
{
    public void ChangeScreenSize()
    {
        if(GetComponent<Toggle>().isOn)
        {
            Screen.fullScreen = true;
        }
        else
        {
            Screen.SetResolution(1920, 1080, false);
        }
    }
}
