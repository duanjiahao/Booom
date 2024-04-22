using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipeUnit
{
    // 药方基本信息
    public int recipeID;
    public string desc;
    public Dictionary<string, int> herbInventory;
    public Dictionary<int, int> needAttributeValue;
}
