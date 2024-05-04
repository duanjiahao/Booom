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
    private Button BtGive;
    private Button BtConfirm;
    private Button BtCancel;
    private RecipeItem recipe;
    private GameObject droppedItem;
    void Awake()
    {
        PagingRoot = this.gameObject;
    }
    void Start()
    {
        BtGive = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtGive");
        BtConfirm = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtConfirm");
        BtCancel = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtCancel");
        BtGive.onClick.AddListener(() =>
        {
            GiveRecipe();
        });
    }
    public void OnDrop(PointerEventData eventData)
    {
        // 从 eventData 获取拖拽的对象
        Debug.Log(eventData.pointerDrag.name);
        droppedItem = Instantiate(eventData.pointerDrag, transform.root);
        droppedItem.GetComponentInChildren<RecipeUnitInfo>().data = eventData.pointerDrag.GetComponentInChildren<RecipeUnitInfo>().data;
        //droppedItem = eventData.pointerDrag;
        //Debug.Log(eventData.pointerDrag.name);
        if (droppedItem != null)
        {
            Debug.Log(droppedItem.name);
            Vector3 newPos = new Vector3(eventData.pointerEnter.transform.position.x, eventData.pointerEnter.transform.position.y + 5, eventData.pointerEnter.transform.position.z);
            droppedItem.transform.position = newPos;
            //TODO没有获取到数据
            
            recipe = droppedItem.GetComponentInChildren<RecipeUnitInfo>().data;
            
        }
    }
    private void GiveRecipe()
    {
        if (recipe == null)
        {
            Debug.LogError("no recipe right now");
            //Destroy(droppedItem);
        }
        else
        {
            Debug.Log("do you want to give him" + recipe.Name + "?");
            RecipeDataManager.Instance.CookRecipe(recipe.Id);
            NPCDataManager.Instance.TreatNPC(recipe);
            NPCDataManager.Instance.CheckResult();
            Destroy(droppedItem);
        }
       
    }
}
