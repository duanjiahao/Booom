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
    public RecipeItem recipe;
    public GameObject droppedItem;
    void Awake()
    {
        PagingRoot = this.gameObject;
    }
    public void OnDisable()
    {
        if (transform.childCount > 0)
        {
            // 如果有，销毁当前的物体及弹窗
            GameObject nowObj = transform.GetChild(0).gameObject;
            nowObj.GetComponentInChildren<RecipeUnitInfo>().DestroyTips();
            Destroy(nowObj);
            recipe = null;
            BtGive.interactable = false;
        }
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
        if (eventData.pointerDrag.GetComponentInChildren<RecipeUnitInfo>().data.Num > 0)
        {
            if (NPCDataManager.Instance.GetNowNPC() == null)
            {
                GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"目前无客人");
                
            }
            else
            {
                // 创建新的物体并设置其位置
                droppedItem = Instantiate(eventData.pointerDrag, UIManager.Instance.jieKePanel.transform);
                droppedItem.transform.Find("quanImg").gameObject.SetActive(false);
                droppedItem.GetComponentInChildren<RecipeUnitInfo>().isDroped = true;
                droppedItem.GetComponentInChildren<RecipeUnitInfo>().data = eventData.pointerDrag.GetComponentInChildren<RecipeUnitInfo>().data;
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
                droppedItem.transform.position = newPos;

                recipe = droppedItem.GetComponentInChildren<RecipeUnitInfo>().data;

                // 将新物体设置为 slot 的子物体
                droppedItem.transform.SetParent(transform);
                BtGive.interactable = true;
                //点击时destroy
                OnClickDestroy();
                
            }
            
        }
        
    }
    private void OnClickDestroy()
    {
        EventTrigger trigger = droppedItem.GetComponent<EventTrigger>() ?? droppedItem.AddComponent<EventTrigger>();
        trigger.triggers.Clear();
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((data) => {
            Destroy(droppedItem);
            recipe = null;
            BtGive.interactable = false;
        });
        trigger.triggers.Add(entry);
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
