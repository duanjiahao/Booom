using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeCreatWindow : MonoBehaviour
{
    public List<HerbEffectAttributeUI> attributeUIs;

    public Button backyardBtn;

    public Toggle toggleHerb;

    public Toggle toggleRecipe;

    public Transform shelves;

    public Transform content;

    public HerbSelectUI herbSelectUI;

    public RecipeSelectUI recipeSelectUI;
}
