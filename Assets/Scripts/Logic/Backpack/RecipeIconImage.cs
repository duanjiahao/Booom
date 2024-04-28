using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class RecipeIconImage : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject InfoPanelObj;
    private GameObject tempItem;
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
        tempItem.transform.position = this.transform.position;
        tempItem.SetActive(true);  // Show the tooltip window
        
        // Update tooltip information based on the item in this slot
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        
        tempItem.SetActive(false);  // Hide the tooltip window
    }

}
