using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudioClick : MonoBehaviour , IPointerDownHandler
{
    public string pa = "Click";
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayAudio(pa, false);
    }
}
