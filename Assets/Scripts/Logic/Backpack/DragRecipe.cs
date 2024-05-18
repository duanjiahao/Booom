using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragRecipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject draggingItem;
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 先获取原始物体的数量
        int RecipeNumber = GetComponentInChildren<RecipeUnitInfo>().data.Num;
        if (NPCDataManager.Instance.GetNowNPC() == null)
        {
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"请先按铃召唤新客人！");
        }
        else if (NPCDataManager.Instance.IsNPCTreat())
        {
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"已经给过药了");
        }
        else
        {
            // 如果数量大于0，则允许拖拽
            if (RecipeNumber > 0)
            {
                // 实例化一个新的物体作为拖拽物体
                draggingItem = Instantiate(gameObject, UIManager.Instance.jieKePanel.transform);
                draggingItem.transform.Find("quanImg").gameObject.SetActive(false);
                draggingItem.GetComponentInChildren<Image>().raycastTarget = false; // 防止新物体被射线检测到
                draggingItem.GetComponentInChildren<RecipeUnitInfo>().data = GetComponentInChildren<RecipeUnitInfo>().data;
                draggingItem.transform.GetComponent<RectTransform>().anchorMin = Vector2.one * 0.5f;
                draggingItem.transform.GetComponent<RectTransform>().anchorMax = Vector2.one * 0.5f;
                draggingItem.transform.GetComponent<RectTransform>().pivot = Vector2.one * 0.5f;
            }
            else
            {
                GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"这个药方已经用光了！");
            }
        }
        
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(draggingItem != null)
        {
            var screenScale = 1920f / Screen.width;
            
            var pos = new Vector2((eventData.position.x - Screen.width / 2f) * screenScale , (eventData.position.y - Screen.height / 2f) * screenScale);
            Debug.Log($"eventData.position:{eventData.position}, screen:{Screen.width} {Screen.height} screenScale:{screenScale} pos:{pos}");
            draggingItem.transform.GetComponent<RectTransform>().anchoredPosition = pos;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            draggingItem.GetComponentInChildren<Image>().raycastTarget = true; // 恢复射线检测
            Destroy(draggingItem);
            draggingItem = null;
        }
    }


}
