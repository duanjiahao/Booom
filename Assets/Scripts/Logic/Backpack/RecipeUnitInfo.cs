using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//药方slot，用于给药方slot赋值，以及实现与弹窗的连接
public class RecipeUnitInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //触发药方弹窗
    public GameObject InfoPanelObj;

    private GameObject PanelItem;
    private GameObject GiveItem;
    public RecipeItem data;
    private Button BtRecipe;
    // Start is called before the first frame update
    void Start()
    {
        
        //药方按钮绑定事件
        //BtRecipe = this.GetComponent<Button>();
        //BtRecipe.onClick.AddListener(() =>
        //{
        //    OnRecipeClicked();
        //});
//        PanelItem = transform.Find("recipeInfoPanel(Clone)").gameObject;
        
        //PanelItem.SetActive(false); // Hide the tooltip window
    }
    private void OnRecipeClicked()
    {
        //GiveItem.transform.position = new Vector3(transform.position.x, transform.position.y + 200, transform.position.z);
        //GiveItem.SetActive(true);
        //PanelItem.SetActive(false);

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
        //PanelItem.GetComponent<RecipeInfoPanel>().Init();
        //SetInfoData(data);
        PanelItem.transform.position = new Vector3(transform.position.x, transform.position.y + 20, transform.position.z);
        //PanelItem.SetActive(true);  // Show the tooltip window
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

}
