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

    //û�п��Ա�ǵ�ʱ�����������ʾ
    private void NoCanMark()
    {
        smm = FindObjectOfType<StartMarkMode>();
        smm.CancelMarkMode();
        ct = FindObjectOfType<CommonTips>();
        ct.GetTipsText("��ҩ����δ֪���ԣ��޷����");
    }

    //��ʼ���
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
