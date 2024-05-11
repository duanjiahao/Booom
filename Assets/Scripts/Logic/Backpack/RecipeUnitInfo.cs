using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//药方slot，用于给药方slot赋值，以及实现与弹窗的连接
public class RecipeUnitInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IDropHandler
{
    //触发药方弹窗
    public GameObject InfoPanelObj;

    private GameObject PanelItem;
    private GameObject GiveItem;
    public RecipeItem data;
    private Button BtRecipe;
    public bool isDroped = false;
    // Start is called before the first frame update
    void Start()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //药方信息弹窗初始化
        if (!PanelItem)
        {
            PanelItem = Instantiate(
InfoPanelObj,
transform.position,
Quaternion.identity, UIManager.Instance.jieKePanel.transform
);
        }
        PanelItem.GetComponent<RecipeInfoUI>().RefreshUI(data);
        PanelItem.transform.position = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Hide the tooltip window
        DestroyTips();
          
    }
    public void DestroyTips()
    {
        if (PanelItem)
        {
            Destroy(PanelItem);
        }
    }
    public void SetData(RecipeItem data)
    {
        this.data = data;
    }
    private void SetInfoData(RecipeItem data)
    {
        Debug.Log(data.Name);
        PanelItem.GetComponent<RecipeInfoPanel>().SetInfoPanelData(data);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right && !isDroped)
        {
            Debug.Log("right");
            GameObject par = this.transform.parent.gameObject;
            GameObject.FindObjectOfType<FastMenu>().FastMenuOn(true, par.GetComponent<RectTransform>().anchoredPosition,this.gameObject);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        if(isDroped)
        {
            transform.parent.gameObject.transform.parent.GetComponent<DropRecipe>().OnDrop(eventData);
        }
    }
}
