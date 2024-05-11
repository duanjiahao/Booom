using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FindFunc;


public class FastMenu : MonoBehaviour,IPointerDownHandler
{

    public GameObject make;
    public GameObject delete;

    public Vector2 makeOffset;
    public Vector2 deleteOffset;

    public GameObject currentItem;

    //private bool isTest;
    //public Vector2 testPos;

    public void JustBeMask(bool isOn)
    {
        GetComponent<Image>().raycastTarget = isOn;
    }

    public void FastCreate()
    {
        if (currentItem != null)
        {
            if (CommonUtils.UseHerbCreateRecipe(currentItem.GetComponent<RecipeUnitInfo>().data, 1))
            {
                GameObject rf = currentItem.transform.parent.gameObject;
                Text quantityText = UnityHelper.GetTheChildNodeComponetScripts<Text>(rf, "quantity");
                quantityText.text = "x " + currentItem.GetComponent<RecipeUnitInfo>().data.Num.ToString();

                FastMenuOn(false, Vector2.zero, null);

                GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"快速制作成功！！");

            }
            else
            {
                GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText("药材不足，无法制作");
            }
        }
    }

    public void FastMenuOn(bool isOn,Vector2 pos, GameObject recipe)
    {
        if(isOn)
        {
            GetComponent<Image>().raycastTarget = isOn;
            GetComponent<Image>().color = new Color(0,0,0,0.2f);
            make.SetActive(true);
            delete.SetActive(true);
            make.GetComponent<RectTransform>().anchoredPosition = pos + makeOffset;
            delete.GetComponent<RectTransform>().anchoredPosition = pos + deleteOffset;
            currentItem = recipe;
            //isTest = true;
            //testPos = pos;
        }
        else
        {
            GetComponent<Image>().raycastTarget = isOn;
            GetComponent<Image>().color = new Color(0, 0, 0, 0f);
            make.SetActive(false);
            delete.SetActive(false);
            currentItem = recipe;
        }
        
    }


    //public void setOFF()
    //{
    //    make.GetComponent<RectTransform>().anchoredPosition = testPos + makeOffset;
    //    delete.GetComponent<RectTransform>().anchoredPosition = testPos + deleteOffset;
    //}

    //private void Update()
    //{
    //    if (isTest)
    //        setOFF();
    //}

    public void OnPointerDown(PointerEventData eventData)
    {
        FastMenuOn(false, Vector2.zero, null);
    }



    public void FastDelete()
    {
        if (currentItem.GetComponent<RecipeUnitInfo>().data != null)
        {
            UIManager.Instance.OpenCreateDeleteWindow(currentItem.GetComponent<RecipeUnitInfo>().data);
            FastMenuOn(false, Vector2.zero, null);
        }
    }

}
