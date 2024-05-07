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
                draggingItem = Instantiate(gameObject, transform.parent);
                draggingItem.GetComponentInChildren<Image>().raycastTarget = false; // 防止新物体被射线检测到
                draggingItem.transform.SetParent(transform.root); // 移动到顶层Canvas，防止被遮挡
                draggingItem.GetComponentInChildren<RecipeUnitInfo>().data = GetComponentInChildren<RecipeUnitInfo>().data;
                draggingItem.transform.GetComponent<RectTransform>().anchorMin = Vector2.zero;
                draggingItem.transform.GetComponent<RectTransform>().anchorMax = Vector2.zero;
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
            var screenScale = 1080f / Screen.height;

            draggingItem.transform.GetComponent<RectTransform>().anchoredPosition = new Vector2(eventData.position.x * screenScale, eventData.position.y * screenScale);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingItem != null)
        {
            draggingItem.GetComponentInChildren<Image>().raycastTarget = true; // 恢复射线检测
            Destroy(draggingItem);
        }
    }
}
