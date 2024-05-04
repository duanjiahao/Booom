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
}
