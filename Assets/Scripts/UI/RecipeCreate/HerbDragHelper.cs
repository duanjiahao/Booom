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

    public ParticleSystem ps;
    
    public void OnDrag(PointerEventData eventData)
    {
        if (MarkState.isOnMarkMode)
        {
            return;
        }
        
        if (_item.Quantity <= 0)
        {
            return;
        }
        
        dragTran.anchoredPosition = new Vector2(eventData.position.x - Screen.width / 2f, eventData.position.y - Screen.height / 2f);

        if (RectTransformUtility.RectangleContainsScreenPoint(targetRect, eventData.position, Camera.main))
        {
            _counter += Mathf.FloorToInt(Time.deltaTime * 1000f);
            Debug.Log(_counter);
            if (_counter >= AddItemInterval)
            {
                ps.Play();
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
            if(ps!=null)
            {
                ps.Pause();
                ps.Clear();
            }
            

            _counter = 0;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (MarkState.isOnMarkMode)
        {
            return;
        }
        
        var uiItem = this.GetComponent<HerbUIItem>();
        _item = uiItem.GetHerbItem();
        if (_item.Quantity <= 0)
        {
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"这个药材已经用光了！");
            return;
        }

        dragItem = GameObject.Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/HerbDragItem"), UIManager.Instance.recipeWindow.transform);
        ps = dragItem.transform.Find("Particle System").GetComponent<ParticleSystem>();

        Color tempcolor = ChangeColor(_item.HerbConfig.id);
        var mainModule = ps.main;
        mainModule.startColor = tempcolor;

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
        
        if (_item.Quantity <= 0)
        {
            return;
        }
        
        Debug.Log("Drag End");
        
        GameObject.Destroy(dragItem);
    }

    public Color ChangeColor(int id)
    {
        switch (id)
        {
            case 1001:
                return Color.white;
            case 1002:
                return Color.red;
            case 1003:
                return Color.black;
            case 1004:
                return Color.blue;
            case 1005:
                return Color.green;
            case 1006:
                return Color.magenta;
            case 1007:
                return Color.yellow;
            case 1008:
                return Color.green;
            case 1009:
                return Color.green;
            case 1010:
                return Color.blue;
            case 1011:
                return Color.red;
            case 1012:
                return Color.blue;
            case 1013:
                return Color.black;
            case 1014:
                return Color.yellow;
            case 1015:
                return Color.blue;
            case 1016:
                return Color.red;

        }
        return Color.white;
    }
}
