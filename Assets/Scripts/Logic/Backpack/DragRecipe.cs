using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragRecipe : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private GameObject draggingItem;
    private Vector3 startPosition;
    private Transform originalParent;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        // 实例化一个新的物体作为拖拽物体
        draggingItem = Instantiate(gameObject, transform.parent);
        draggingItem.GetComponentInChildren<Image>().raycastTarget = false; // 防止新物体被射线检测到
        //draggingItem.GetComponent<CanvasGroup>().blocksRaycasts = false; // 确保在拖动时不会阻挡射线
        //startPosition = draggingItem.transform.position;
        //originalParent = draggingItem.transform.parent;
        draggingItem.transform.SetParent(transform.root); // 移动到顶层Canvas，防止被遮挡
        draggingItem.GetComponentInChildren<RecipeUnitInfo>().data = this.GetComponentInChildren<RecipeUnitInfo>().data;
        // 将拖拽副本的transform设置为拖拽中的对象
        //eventData.pointerDrag = draggingItem;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
        draggingItem.transform.position = Input.mousePosition; // 跟随鼠标移动
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(eventData.pointerDrag.name);
        draggingItem.GetComponentInChildren<Image>().raycastTarget = true; // 恢复射线检测
        // 检查是否放置在正确的对象上
        if (eventData.pointerEnter != null && eventData.pointerEnter.CompareTag("TargetSlot"))
        {
            // 询问是否给药
            Debug.Log("Dropped on the correct slot");
            // 可以在这里处理放置成功的逻辑
            Destroy(draggingItem);
            //draggingItem.transform.position = eventData.pointerEnter.transform.position;
        }
        else
        {
            Debug.Log("Dropped on incorrect slot or outside");
            // 销毁拖拽副本或返回到原始位置
            Destroy(draggingItem);
        }
    }
}
