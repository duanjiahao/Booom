using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbPickItem : MonoBehaviour
{
    public Image icon;

    public TextMeshProUGUI num;

    public TextMeshProUGUI name;

    public void SetData(HerbWeightData data)
    {
        var config = ConfigManager.Instance.GetConfig<HerbsConfig>(data.HerbId);

        var weight = data.Weight;
        string iconPath = null;
        if (weight > 0 && weight <= 20)
        {
            iconPath = config.heapPath1;
        }
        else if (weight > 10 && weight <= 40)
        {
            iconPath = config.heapPath2;
        }
        else if (weight > 40) 
        {
            iconPath = config.heapPath3;
        }

        icon.sprite = Resources.Load<Sprite>(iconPath); // 这里可能会有性能问题

        name.text = config.name;

        num.text = CommonUtils.GetWeightStr(data.Weight);
    }
}
