using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HerbUIItem : MonoBehaviour
{
    public Button btn;

    public Image shadow_normal;

    public Image shadow_selected;

    public Image shadow_draging;

    public Image icon;

    public TextMeshProUGUI num;

    private int _currentState;
    
    public void InitUI(HerbItem item)
    {
        SetState(1);
        shadow_normal.gameObject.SetActive(true);
        shadow_selected.gameObject.SetActive(false);
        shadow_draging.gameObject.SetActive(false);

        icon.sprite = Resources.Load<Sprite>(item.HerbConfig.iconPath);
        num.text = CommonUtils.GetWeightStr(item.Quantity);
    }

    public void SetState(int state)
    {
        _currentState = state;
        switch (state)
        {
            case 1:
                shadow_normal.gameObject.SetActive(true);
                shadow_selected.gameObject.SetActive(false);
                shadow_draging.gameObject.SetActive(false);
                break;
            case 2:
                shadow_normal.gameObject.SetActive(false);
                shadow_selected.gameObject.SetActive(true);
                shadow_draging.gameObject.SetActive(false);
                break;
            case 3:
                shadow_normal.gameObject.SetActive(false);
                shadow_selected.gameObject.SetActive(false);
                shadow_draging.gameObject.SetActive(true);
                break;
        }
    }

    public int GetState()
    {
        return _currentState;
    }
}
