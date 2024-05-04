using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeDeleteWindow : MonoBehaviour
{
    public TextMeshProUGUI desc;

    public Button confirmBtn;

    public Button cancelBtn;

    private RecipeItem _recipeItem;
    
    private void OnEnable()
    {
        confirmBtn.onClick.AddListener(OnConfirmBtnClicked);
        cancelBtn.onClick.AddListener(OnCancelBtnClicked);
    }

    private void OnCancelBtnClicked()
    {
        this.gameObject.SetActive(false);
    }

    private void OnConfirmBtnClicked()
    {
        RecipeDataManager.Instance.DeleteRecipe(_recipeItem.Id);
        this.gameObject.SetActive(false);
        UIManager.Instance.recipeWindow.RefreshContent(false);
    }

    public void RefreshUI(RecipeItem recipeItem)
    {
        _recipeItem = recipeItem;
        desc.text = $"确认删除药方{recipeItem.Name}吗？";
    }

    private void OnDisable()
    {
        confirmBtn.onClick.RemoveListener(OnConfirmBtnClicked);
        cancelBtn.onClick.RemoveListener(OnCancelBtnClicked);
    }
}
