using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FindFunc;
//放置药方slot，获取药方信息及给药
public class DropRecipe : MonoBehaviour, IDropHandler
{
    private GameObject PagingRoot;
    public Button BtGive;
    private RecipeItem recipe;
    private GameObject droppedItem;
    void Awake()
    {
        PagingRoot = this.gameObject;
    }
    void Start()
    {
        BtGive.interactable = false;
        //BtGive = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtGive");
        BtGive.onClick.AddListener(() =>
        {
            GiveRecipe();
        });
    }
    public void OnDrop(PointerEventData eventData)
    {
        // 从 eventData 获取拖拽的对象
        Debug.Log(eventData.pointerDrag.name);
        // 检查当前 slot 是否已经有物体
        if (transform.childCount > 0)
        {
            // 如果有，销毁当前的物体及弹窗
            GameObject nowObj = transform.GetChild(0).gameObject;
            nowObj.GetComponentInChildren<RecipeUnitInfo>().DestroyTips();
            Destroy(nowObj);
        }

        // 创建新的物体并设置其位置
        droppedItem = Instantiate(eventData.pointerDrag, UIManager.Instance.jieKePanel.transform);
        droppedItem.GetComponentInChildren<RecipeUnitInfo>().data = eventData.pointerDrag.GetComponentInChildren<RecipeUnitInfo>().data;

        if (droppedItem != null)
        {
            Debug.Log(droppedItem.name);
            Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            droppedItem.transform.position = newPos;

            recipe = droppedItem.GetComponentInChildren<RecipeUnitInfo>().data;

            // 将新物体设置为 slot 的子物体
            droppedItem.transform.SetParent(transform);
        }
        if (NPCDataManager.Instance.GetNowNPC() != null)
        {
            BtGive.interactable = true;
            
        }
        
    }
    private void GiveRecipe()
    {
        if (recipe.Num > 0)
        {
            RecipeDataManager.Instance.UseRecipe(recipe.Id);
            NPCDataManager.Instance.TreatNPC(recipe);
            NPCDataManager.Instance.CheckResult();
            BackpackPanelControl bpPanel = FindObjectOfType<BackpackPanelControl>();
            bpPanel.RefreshRecipe();
            Destroy(droppedItem);
            BtGive.interactable = false;
            jiekePanel jkPanel = FindObjectOfType<jiekePanel>();
            jkPanel.PlayEndingDialogue();
        }
        else
        {
            GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"无剩余药方");
        }
       
    }
}
