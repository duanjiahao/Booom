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
    private GameObject tempItem;
    public RecipeItem data;
    // Start is called before the first frame update
    void Start()
    {
        tempItem = Instantiate(
InfoPanelObj,
transform.position,
Quaternion.identity,transform
);
        tempItem.SetActive(false); // Hide the tooltip window
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tempItem.GetComponent<RecipeInfoPanel>().Init();
        Debug.Log(data.Name);
        SetInfoData(data);
        tempItem.transform.position = this.transform.position;
        tempItem.SetActive(true);  // Show the tooltip window
        
        // Update tooltip information based on the item in this slot
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        tempItem.SetActive(false);  // Hide the tooltip window
    }
    public void SetData(RecipeItem data)
    {
        this.data = data;
    }
    private void SetInfoData(RecipeItem data)
    {
        Debug.Log(data.Name);
        tempItem.GetComponent<RecipeInfoPanel>().SetInfoPanelData(data);
    }

}
