using System.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeRenameWindow : MonoBehaviour
{
    public Transform effectContainer;

    public Transform herbContainer;
    
    public TMP_InputField nameInput;

    public Button createBtn;

    public Button cancelBtn;

    private RecipeItem _recipeItem;

     private void OnEnable()
    {
        createBtn.onClick.AddListener(OnCreateBtnClicked);
        cancelBtn.onClick.AddListener(OnCancelBtnClicked);
    }
     
    private void OnCreateBtnClicked()
    {
        RecipeDataManager.Instance.ChangeRecipeName(_recipeItem.Id, nameInput.text);
        
        this.gameObject.SetActive(false);
        
        UIManager.Instance.recipeWindow.RefreshContent(false, _recipeItem);
        UIManager.Instance.recipeWindow.recipeSelectUI.RefreshUI(_recipeItem);
    }

    public void RefreshUI(RecipeItem recipeItem)
    {
        _recipeItem = recipeItem;
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

    private void OnCancelBtnClicked()
    {
        this.gameObject.SetActive(false);
    }
}
