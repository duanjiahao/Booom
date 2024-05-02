using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbNeedItem : MonoBehaviour
{
    public Image icon;

    public TextMeshProUGUI num;

    public void RefreshUI(HerbRecipeInfo info, int times) 
    {
        var config = ConfigManager.Instance.GetConfig<HerbsConfig>(info.HerbId);

        icon.sprite = Resources.Load<Sprite>(config.iconPath);

        var herbItem = HerbDataManager.Instance.GetHerbItemByID(info.HerbId);

        var needWeight = times * info.Weight;

        var hasWeight = herbItem?.Quantity ?? 0;

        var surfix = string.Empty;
        var prefix = string.Empty;
        if (hasWeight < needWeight) 
        {
            prefix = "<color=FF2C2C>";
            surfix = "</color>";
        }

        num.text = $"{CommonUtils.GetWeightStr(needWeight)}/{prefix}{CommonUtils.GetWeightStr(hasWeight)}{surfix}";
    }
}
