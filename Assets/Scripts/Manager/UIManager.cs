using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleMono<UIManager>
{
    public RecipeCreatWindow recipeWindow;

    public RecipeCompleteWindow createRecipeWindow;

    public RecipeDeleteWindow recipeDeleteWindow;

    public RecipeRenameWindow recipeRenameWindow;

    public BackyardWindow backyardWindow;

    public UICollectionWindow collectionWindow;

    public jiekePanel jieKePanel;
    public bookPanel bingliWindow;

    public UIBeginPanel beginPanel;

    public UISettingPanel settingPanel;

    public CommonUI commonUI;
    public void OpenCreateRecipeWindow(List<HerbWeightData> datas)
    {
        createRecipeWindow.gameObject.SetActive(true);
        createRecipeWindow.RefreshUI(datas);
    }
    
    public void OpenCreateDeleteWindow(RecipeItem item)
    {
        recipeDeleteWindow.gameObject.SetActive(true);
        recipeDeleteWindow.RefreshUI(item);
    }
    
    public void OpenRenameWindow(RecipeItem item)
    {
        recipeRenameWindow.gameObject.SetActive(true);
        recipeRenameWindow.RefreshUI(item);
    }

    public void OpenJiKeWindow()
    {
        jieKePanel.gameObject.SetActive(true);
    }

    public void OpenBingLiWindow()
    {
        bingliWindow.gameObject.SetActive(true);
        bingliWindow.RefreshBookData();
    }

    public void OpenRecipeWindow()
    {
        recipeWindow.gameObject.SetActive(true);
    }

    public void OpenBackyardWindow()
    {
        backyardWindow.gameObject.SetActive(true);
    }

    public void OpenCollectionWindow()
    {
        collectionWindow.gameObject.SetActive(true);
    }
    public void OpenSettingWindow()
    {
        settingPanel.gameObject.SetActive(true);
    }

    public void OpenBeginWindow()
    {
        CloseAllWindow();
        beginPanel.gameObject.SetActive(true);
        recipeWindow.gameObject.SetActive(false);
        backyardWindow.gameObject.SetActive(false);
        jieKePanel.gameObject.SetActive(false);
    }

    public void CloseAllWindow()
    {
        recipeWindow.gameObject.SetActive(false);
        createRecipeWindow.gameObject.SetActive(false);
        recipeDeleteWindow.gameObject.SetActive(false);
        recipeRenameWindow.gameObject.SetActive(false);
        backyardWindow.gameObject.SetActive(false);
        collectionWindow.gameObject.SetActive(false);
        jieKePanel.gameObject.SetActive(false);
        bingliWindow.gameObject.SetActive(false);
        beginPanel.gameObject.SetActive(false);
        settingPanel.gameObject.SetActive(false);
    }
}
