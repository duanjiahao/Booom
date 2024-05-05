using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeCompleteWindow : MonoBehaviour
{
    public Transform recipeContainer;

    public Transform effectContainer;

    public TMP_InputField nameInput;

    public Button createBtn;

    public Button cancelBtn;

    private List<HerbWeightData> _dataList;

    private void OnEnable()
    {
        createBtn.onClick.AddListener(OnCreateBtnClicked);
        cancelBtn.onClick.AddListener(OnCancelBtnClicked);
    }

    public void RefreshUI(List<HerbWeightData> dataList)
    {
        _dataList = dataList;
        List<EffectInfoData> effectList = new List<EffectInfoData>();
        int[] attributes = { 0, 0, 0, 0 };
        bool[] visibles = { true, true, true, true };
        
        CommonUtils.GetAttributeValueAndVisible(dataList, attributes, visibles);
        
        for (int i = 0; i < 4; i++)
        {
            var child = recipeContainer.GetChild(i);
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

    private void OnCancelBtnClicked()
    {
        this.gameObject.SetActive(false);
    }

    private void OnCreateBtnClicked()
    {
        List<HerbRecipeInfo> infos = new List<HerbRecipeInfo>();
        foreach (var data in _dataList)
        {
            infos.Add(new HerbRecipeInfo(){HerbId = data.HerbId, Weight = data.Weight});
            
            HerbDataManager.Instance.UseHerb(data.HerbId, data.Weight);
        }
        
        RecipeDataManager.Instance.CreateRecipe(RecipeDataManager.Instance.GenerateId(), nameInput.text, 1, infos);
        this.gameObject.SetActive(false);
        
        UIManager.Instance.recipeWindow.RefreshUI();
        
        GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText($"{nameInput.text} 创建成功！");
    }

    private void OnDisable()
    {
        createBtn.onClick.RemoveListener(OnCreateBtnClicked);
        cancelBtn.onClick.RemoveListener(OnCancelBtnClicked);
    }
}
