using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIWeightDesc : MonoBehaviour
{
    public TextMeshProUGUI name;

    public TextMeshProUGUI num;

    public void RefreshUI(HerbWeightData data)
    {
        name.text = ConfigManager.Instance.GetConfig<HerbsConfig>(data.HerbId).name;
        num.text = $"{data.Weight / 10}''{data.Weight % 10}'";
    }
}
