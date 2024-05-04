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
        
        UIManager.Instance.recipeWindow.RefreshContent(true);
    }

    private void OnDisable()
    {
        createBtn.onClick.RemoveListener(OnCreateBtnClicked);
        cancelBtn.onClick.RemoveListener(OnCancelBtnClicked);
    }
}
