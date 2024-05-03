using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUIItem : MonoBehaviour
{
    public Button btn;

    public Image icon;

    public Image icon_selected;

    public TextMeshProUGUI num;

    public TextMeshProUGUI name;

    public void InitUI(RecipeItem recipeItem)
    {
        icon.gameObject.SetActive(true);
        icon_selected.gameObject.SetActive(false);

        num.text = recipeItem.Num.ToString();
        name.text = recipeItem.Name;
    }

    public void SetSelect(bool select)
    {
        icon.gameObject.SetActive(!select);
        icon_selected.gameObject.SetActive(select);
    }
}
