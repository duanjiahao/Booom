using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RecipeInfoUI : MonoBehaviour
{
    public Transform effectContainer;

    public Transform herbContainer;

    public TextMeshProUGUI nameTxt;

    
    public void RefreshUI(RecipeItem recipeItem)
    {
        nameTxt.text = recipeItem.Name;
        
        var dataList = new List<HerbWeightData>();
        foreach (var herb in recipeItem.HerbList)
        {
            dataList.Add(new HerbWeightData(){ HerbId = herb.HerbId, Weight = herb.Weight});
        }
        
        List<EffectInfoData> effectList = new List<EffectInfoData>();
        int[] attributes = { 0, 0, 0, 0 };
        bool[] visibles = { true, true, true, true };
        
        CommonUtils.GetAttributeValueAndVisible(dataList, attributes, visibles);
        
        for (int i = 1; i <= 4; i++)
        {
            var con = CommonUtils.GetEffectAttributeConfig((EffectAttributeType)i, attributes[i - 1]);
            if (con != null)
            {
                effectList.Add(new EffectInfoData(con, visibles[i - 1]));
                effectList.Add(new EffectInfoData(CommonUtils.GetCorrespondBadEffectConfig(con), visibles[i - 1]));
            }
        }

        for (int i = 0; i < 4; i++)
        {
            var child = herbContainer.GetChild(i);
            if (i < dataList.Count)
            {
                child.gameObject.SetActive(true);
                var mono = child.GetComponent<UIWeightDesc>();
                mono.RefreshUI(dataList[i]);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }

        bool hasUnkown = visibles.Contains(false);
        for (int i = 0; i < 4; i++)
        {
            var child = effectContainer.GetChild(i);
            var goodEffectIndex = i * 2;
            var badEffectIndex = i * 2 + 1;
            if (badEffectIndex < effectList.Count)
            {
                child.gameObject.SetActive(true);
                var mono = child.GetComponent<UIHerbEffectDesc>();
                mono.RefreshUI(effectList[goodEffectIndex].EffectAxisConfig, effectList[badEffectIndex].EffectAxisConfig, visibles[i]);
            }
            else
            {
                if (hasUnkown)
                {
                    child.gameObject.SetActive(true);
                    var mono = child.GetComponent<UIHerbEffectDesc>();
                    mono.RefreshUI(null, null, false);
                    hasUnkown = false;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
    }
}
