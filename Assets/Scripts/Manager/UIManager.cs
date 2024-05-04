using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingleMono<UIManager>
{
    public RecipeCreatWindow recipeWindow;

    public RecipeCompleteWindow createRecipeWindow;

    public RecipeDeleteWindow recipeDeleteWindow;

    public RecipeRenameWindow recipeRenameWindow;
    
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
}
