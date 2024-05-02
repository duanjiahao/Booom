using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CancelMarkMode : MonoBehaviour, IPointerDownHandler
{
    public StartMarkMode startMarkMode;

    private void Awake()
    {
        startMarkMode = GameObject.Find("MarkModeManager").GetComponent<StartMarkMode>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("cancel");
            startMarkMode.CancelMarkMode();
        }
    }
}
