using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class HerbIconImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    //弹窗预制体
    public GameObject InfoPanelObj;
    private GameObject tempItem;
    public BackpackHerbItem data;
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
        tempItem.GetComponent<HerbInfoPanel>().Init();
        SetInfoData(data);
        tempItem.transform.position = this.transform.position;
        tempItem.SetActive(true);  // Show the tooltip window

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        tempItem.SetActive(false);  // Hide the tooltip window
    }
    public void SetData(BackpackHerbItem data)
    {
        this.data = data;
    }
    private void SetInfoData(BackpackHerbItem data)
    {
        tempItem.GetComponent<HerbInfoPanel>().SetHerbInfo(data);
    }

}
