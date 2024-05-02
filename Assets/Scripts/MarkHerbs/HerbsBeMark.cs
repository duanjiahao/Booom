using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbsBeMark : MonoBehaviour
{
    public HerbItem currentData;

    public GameObject markHerbsPanel;

    public CommonTips ct;
    public StartMarkMode smm;

    public void isMarkMode()
    {
        if (MarkState.isOnMarkMode)
            Marking();
    }

    public void Marking()
    {
        HerbUnitInfo hui = GetComponent<HerbUnitInfo>();
        currentData = hui.data;

        for (int i = 0; i < currentData.IsVisible.Length; i++)
        {
            if(!currentData.IsVisible[i])
            {
                ToMark(currentData);
                return;
            }
        }

        NoCanMark();
    }

    //没有可以标记的时候进行文字提示
    private void NoCanMark()
    {
        smm = FindObjectOfType<StartMarkMode>();
        smm.CancelMarkMode();
        ct = FindObjectOfType<CommonTips>();
        ct.GetTipsText("此药材无未知属性，无法标记");
    }

    //开始标记
    private void ToMark(HerbItem data)
    {
        Debug.Log(data.HerbConfig.name);

        GetHerbsMarked[] components = Resources.FindObjectsOfTypeAll<GetHerbsMarked>();
        foreach (var component in components)
        {
            if (component.gameObject.scene.IsValid())
            {
                markHerbsPanel = component.gameObject;
                break;
            }
        }
        
        markHerbsPanel.SetActive(true);
        markHerbsPanel.GetComponent<GetHerbsMarked>().GetHerbs(data);

    }
 


}
