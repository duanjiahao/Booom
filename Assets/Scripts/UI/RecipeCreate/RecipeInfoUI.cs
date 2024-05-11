using System.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

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

        int unkownIndex = -1;
        bool hasUnkown = visibles.Contains(false);
        for (int i = 0; i < 4; i++)
        {
            var child = effectContainer.GetChild(i);
            var attributeConfig = CommonUtils.GetEffectAttributeConfig((EffectAttributeType)(i + 1), attributes[i]);
            if (visibles[i] && attributeConfig != null)
            {
                child.gameObject.SetActive(true);
                var mono = child.GetComponent<UIHerbEffectDesc>();
                mono.RefreshUI(attributeConfig, CommonUtils.GetCorrespondBadEffectConfig(attributeConfig), true);
            }
            else
            {
                if (hasUnkown)
                {
                    child.gameObject.SetActive(true);
                    var mono = child.GetComponent<UIHerbEffectDesc>();
                    mono.RefreshUI(null, null, false);
                    hasUnkown = false;
                    unkownIndex = i;
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        }
        
        if (unkownIndex >= 0)
        {
            var child = effectContainer.GetChild(unkownIndex);
            child.SetAsLastSibling();
        }
    }




}
