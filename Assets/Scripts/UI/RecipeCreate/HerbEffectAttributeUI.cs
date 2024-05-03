using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HerbEffectAttributeUI : MonoBehaviour
{
    public Image fill;

    public List<Image> lines;

    public Image numBg;

    public TextMeshProUGUI num;

    public GameObject icon;

    public GameObject icon_gray;

    public List<HerbEffectDescItem> herbEffectDescItemList;

    public GameObject fill_gray;

    public GameObject questionIcon;

    public void InitUI(EffectAttributeType type)
    {
        RefreshUI(type, 0, true);
    }

    public void RefreshUI(EffectAttributeType type, int value, bool visible) 
    {
        numBg.gameObject.SetActive(visible);
        fill_gray.gameObject.SetActive(!visible);
        icon.gameObject.SetActive(visible);
        icon_gray.SetActive(!visible);
        questionIcon.SetActive(!visible);

        var sprite = Resources.Load<Sprite>(visible ? "UI/line_normal" : "UI/line_gray");
        foreach (var line in lines)
        {
            line.sprite = sprite;
        }

        var effectAxisConfigList = ConfigManager.Instance.GetConfigListWithFilter<EffectAxisConfig>((config) => 
        {
            return config.attributes == (int)type;
        });

        List<EffectAxisConfig> goodEffectList = new List<EffectAxisConfig>();
        List<EffectAxisConfig> badEffectList = new List<EffectAxisConfig>();
        List<int> effectValueList = new List<int>();

        foreach (var config in effectAxisConfigList)
        {
            if (config.isPositive)
            {
                goodEffectList.Add(config);
                effectValueList.Add(config.value);
            }
            else 
            {
                badEffectList.Add(config);
            }
        }

        effectValueList.Sort();

        num.text = value.ToString();

        var fillAmount = Mathf.InverseLerp(-50f, 100f, value);
        fill.fillAmount = fillAmount;

        bool needReserve = type == EffectAttributeType.Han || type == EffectAttributeType.Re;
        numBg.GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(120f * (needReserve ? -1f : 1f), -120f * (needReserve ? -1f : 1f), fillAmount), 0);

        for (int i = 0; i < effectValueList.Count; i++)
        {
            var effectValue = effectValueList[i];
            var effectAmount = Mathf.InverseLerp(-50f, 100f, effectValue);
            lines[i].GetComponent<RectTransform>().anchoredPosition = new Vector2(Mathf.Lerp(125f * (needReserve ? -1f : 1f), -125f * (needReserve ? -1f : 1f), effectAmount), 0);
        }

        var effectIndex = -1;
        if (visible)
        {
            for (int i = 0; i < effectValueList.Count; i++)
            {
                var effectValue = effectValueList[i];
                if (effectValue < 0 && value <= effectValue)
                {
                    effectIndex = i;
                    break;
                }
                else if (effectValue > 0) 
                {
                    if (value >= effectValue && (i == effectValueList.Count - 1 || value < effectValueList[i + 1]))
                    {
                        effectIndex = i;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < herbEffectDescItemList.Count; i++)
        {
            var item = herbEffectDescItemList[i];
            item.RefreshUI(goodEffectList[i], badEffectList[i], effectIndex == i);
        }
    }
}
