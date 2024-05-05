using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;using UnityEngine.UI;

public class HerbDragHelper : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameObject dragItem;

    private RectTransform dragTran;

    private RectTransform targetRect;

    private HerbItem _item;

    private const int AddItemInterval = 100;

    private int _counter;
    
    public void OnDrag(PointerEventData eventData)
    {
        if (MarkState.isOnMarkMode)
        {
            return;
        }
        
        Debug.Log($"Dragging {eventData.position}");

        dragTran.anchoredPosition = new Vector2(eventData.position.x - Screen.width / 2f, eventData.position.y - Screen.height / 2f);

        if (RectTransformUtility.RectangleContainsScreenPoint(targetRect, eventData.position, Camera.main))
        {
            _counter += Mathf.FloorToInt(Time.deltaTime * 1000f);
            if (_counter >= AddItemInterval)
            {
                UIManager.Instance.recipeWindow.herbSelectUI.AddWeight(new HerbWeightData(){ HerbId = _item.HerbConfig.id, Weight = 1});
                
                var currentWeightDataList = UIManager.Instance.recipeWindow.herbSelectUI.CurrentWeightDataList;

                bool[] visibleList = { true,true,true,true };
                int[] attributes = { 0, 0, 0, 0 };
                CommonUtils.GetAttributeValueAndVisible(currentWeightDataList, attributes, visibleList);
        
                for (int i = 0; i < UIManager.Instance.recipeWindow.attributeUIs.Count; i++)
                {
                    var attributeUI = UIManager.Instance.recipeWindow.attributeUIs[i];
                    attributeUI.RefreshUI((EffectAttributeType)(i+1), attributes[i], visibleList[i]);
                }
                
                _counter = 0;
            }
        }
        else
        {
            _counter = 0;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MarkState.isOnMarkMode)
        {
            return;
        }

        Debug.Log("Drag Start");

        dragItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/HerbDragItem"), UIManager.Instance.recipeWindow.transform);

        var uiItem = this.GetComponent<HerbUIItem>();
        _item = uiItem.GetHerbItem();

        dragItem.transform.Find("icon").GetComponent<Image>().sprite = uiItem.icon.sprite;
        
        dragTran = dragItem.gameObject.GetComponent<RectTransform>();

        targetRect = UIManager.Instance.recipeWindow.herbSelectUI.GetComponent<RectTransform>();
        
        UIManager.Instance.recipeWindow.OnHerbItemClicked(_item, uiItem);

        _counter = 0;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (MarkState.isOnMarkMode)
        {
            return;
        }
        
        Debug.Log("Drag End");
        
        GameObject.Destroy(dragItem);
    }
}
