using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HerbItemHoverHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public HerbInfoPanel herbInfoPanel;

    public HerbUIItem herbUIItem;

    public Vector2 offset;


    public void OnPointerEnter(PointerEventData eventData)
    {
        herbInfoPanel.gameObject.SetActive(true);

        herbInfoPanel.gameObject.GetComponent<RectTransform>().anchoredPosition =
            herbUIItem.GetComponent<RectTransform>().anchoredPosition + offset;
        
        herbInfoPanel.Init();
        herbInfoPanel.SetHerbInfo(herbUIItem.GetHerbItem());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        herbInfoPanel.gameObject.SetActive(false);
    }
}
