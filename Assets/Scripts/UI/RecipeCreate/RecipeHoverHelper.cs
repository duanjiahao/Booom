using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RecipeHoverHelper : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RecipeInfoUI recipeInfoPanel;

    public RecipeUIItem recipeUIItem;

    public Vector2 offset;


    public void OnPointerEnter(PointerEventData eventData)
    {
        recipeInfoPanel.gameObject.SetActive(true);

        recipeInfoPanel.gameObject.GetComponent<RectTransform>().anchoredPosition =
            recipeUIItem.GetComponent<RectTransform>().anchoredPosition + offset;
        
        recipeInfoPanel.RefreshUI(recipeUIItem.GetRecipeItem());
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        recipeInfoPanel.gameObject.SetActive(false);
    }
}
