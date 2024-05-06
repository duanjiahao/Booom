using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
//药材slot，用于给slot赋值，以及实现与弹窗的连接
public class HerbUnitInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    //弹窗预制体
    public GameObject InfoPanelObj;
    private GameObject tempItem;
    public HerbItem data;
    // Start is called before the first frame update
    void Start()
    {
        tempItem = Instantiate(
InfoPanelObj,
transform.position,
Quaternion.identity, UIManager.Instance.jieKePanel.transform
);
        //tempItem = InfoPanelObj;
        tempItem.SetActive(false); // Hide the tooltip window
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        tempItem.GetComponent<HerbInfoPanel>().Init();
        SetInfoData(data);
        tempItem.transform.position = new Vector3(transform.position.x, transform.position.y+20, transform.position.z);
        tempItem.SetActive(true);  // Show the tooltip window

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        tempItem.SetActive(false);  // Hide the tooltip window
    }
    public void SetData(HerbItem data)
    {
        this.data = data;
    }
    private void SetInfoData(HerbItem data)
    {
        tempItem.GetComponent<HerbInfoPanel>().SetHerbInfo(data);
    }

}
