using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class RecipeSelectUI : MonoBehaviour
{
    public TextMeshProUGUI name;

    public Transform herbNeedItemContainer;

    public Slider slider;

    public Button upBtn;

    public Button downBtn;

    public TextMeshProUGUI minNum;

    public TextMeshProUGUI maxNum;

    public TextMeshProUGUI num;

    public Button delBtn;

    public Button createBtn;

    public Button renameBtn;

    private int _currentNum;

    private RecipeItem _recipeItem;

    private void OnEnable()
    {
        upBtn.onClick.AddListener(OnUpBtnClicked);
        downBtn.onClick.AddListener(OnDownBtnClicked);
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        delBtn.onClick.AddListener(OnDelBtnClicked);
        createBtn.onClick.AddListener(OnCreateBtnClicked);
        renameBtn.onClick.AddListener(OnRenameBtnClicked);
    }

    private void OnRenameBtnClicked()
    {
        if (_recipeItem != null)
        {
            UIManager.Instance.OpenRenameWindow(_recipeItem);
        }
    }

    private void OnCreateBtnClicked()
    {
        if (_recipeItem != null)
        {
            if (CommonUtils.UseHerbCreateRecipe(_recipeItem, _currentNum))
            {
                UIManager.Instance.recipeWindow.RefreshContent(false, _recipeItem);
            
                RefreshUI(_recipeItem);
            }
            else
            {
                GameObject.Find("CommonUI").GetComponent<CommonTips>().GetTipsText("药材重量不足，无法制作药方");
            }
        }
    }

    private void OnDelBtnClicked()
    {
        if (_recipeItem != null)
        {
            UIManager.Instance.OpenCreateDeleteWindow(_recipeItem);
        }
    }

    private void OnSliderValueChanged(float value)
    {
        var valueInt = (int)value;

        if (_currentNum == valueInt) 
        {
            return;
        }

        _currentNum = valueInt;

        RefreshItem();
    }

    private void OnDownBtnClicked()
    {
        slider.value = _currentNum - 1;
    }

    private void OnUpBtnClicked()
    {
        slider.value = _currentNum + 1;
    }

    private void OnDisable()
    {
        upBtn.onClick.RemoveListener(OnUpBtnClicked);
        downBtn.onClick.RemoveListener(OnDownBtnClicked);
        slider.onValueChanged.RemoveListener(OnSliderValueChanged);
        delBtn.onClick.RemoveListener(OnDelBtnClicked);
        createBtn.onClick.RemoveListener(OnCreateBtnClicked);
        renameBtn.onClick.RemoveListener(OnRenameBtnClicked);
    }

    public void InitUI()
    {
        _recipeItem = null;
        name.text = string.Empty;
        
        slider.wholeNumbers = true;
        slider.minValue = 1;
        slider.maxValue = 1;
        _currentNum = 1;
        
        herbNeedItemContainer.DestroyAllChildren();
        num.text = 1.ToString();

        minNum.text = 1.ToString();
        maxNum.text = 1.ToString();
    }

    public void RefreshUI(RecipeItem recipeItem) 
    {
        _recipeItem = recipeItem;

        name.text = recipeItem.Name;

        int maxNumber = int.MaxValue;
        for (int i = 0; i < recipeItem.HerbList?.Count; i++)
        {
            var herb = recipeItem.HerbList[i];
            var hasWeight = HerbDataManager.Instance.GetHerbItemByID(herb.HerbId)?.Quantity ?? 0;

            var number = hasWeight / herb.Weight;
            maxNumber = Mathf.Min(maxNumber, number);
        }

        var minNumb = 1;
        var maxNumb = Mathf.Max(1, maxNumber);

        slider.wholeNumbers = true;
        slider.minValue = minNumb;
        slider.maxValue = maxNumb;
        _currentNum = minNumb;

        minNum.text = minNumb.ToString();
        maxNum.text = maxNumb.ToString();

        RefreshItem();
    }

    public void RefreshItem() 
    {
        num.text = _currentNum.ToString();

        for (int i = 0; i < _recipeItem?.HerbList?.Count; i++) 
        {
            var herb = _recipeItem.HerbList[i];
            if (herbNeedItemContainer.childCount > i)
            {
                var item = herbNeedItemContainer.GetChild(i);
                item.GetComponent<HerbNeedItem>().RefreshUI(herb, _currentNum);
            }
            else 
            {
                var item = Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/HerbNeedItem"), herbNeedItemContainer);
                item.GetComponent<HerbNeedItem>().RefreshUI(herb, _currentNum);
            }
        }
    }
}
