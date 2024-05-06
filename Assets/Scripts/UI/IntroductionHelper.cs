using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class IntroductionHelper : MonoBehaviour, IPointerClickHandler
{
    public List<Sprite> Sprites;
    
    private int _index = 0;
    
    private void OnEnable()
    {
        _index = 0;
        GetComponent<Image>().sprite = Sprites[0];
        UIManager.Instance.commonUI.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        UIManager.Instance.commonUI.gameObject.SetActive(true);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        _index++;
        if (_index < Sprites.Count)
        {
            GetComponent<Image>().sprite = Sprites[_index];
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
