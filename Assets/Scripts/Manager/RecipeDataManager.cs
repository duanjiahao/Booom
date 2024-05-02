using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
//药方数据管理单例
public class RecipeDataManager : Singleton<RecipeDataManager>
{
    public List<RecipeItem> recipeInventory = new List<RecipeItem>();
    public List<EffectAxisConfig> effectInventory = new List<EffectAxisConfig>();
    protected override void Init()
    {
        List<HerbRecipeInfo> herbList = new List<HerbRecipeInfo>();
        herbList.Add(new HerbRecipeInfo() { HerbId = 1001, Weight = 10 }); ;
        herbList.Add(new HerbRecipeInfo() { HerbId = 1002, Weight = 5 } );
        RecipeItem recipeItem = new RecipeItem(1, "first recipe", 12, herbList);
        recipeInventory.Add(recipeItem);
        RecipeItem recipeItem2 = new RecipeItem(2, "second recipe", 32, herbList);
        recipeInventory.Add(recipeItem2);
    }

    public void CreateRecipe(int id, string name, int num, List<HerbRecipeInfo> infos)
    {
        //添加药方
        var tempItem = new RecipeItem(id, name, num, infos);
        recipeInventory.Add(tempItem);
    }
    public void UseRecipe(int id)
    {
        //给出药方，默认一次只能用一个
        //根据id查找
        //foreach (var item in recipeInventory)
        //{
        //    if (item.ID == id)
        //    {
        //        if (item.Quantity >= 1)
        //        {
        //            item.Quantity -= 1;
        //        }
                
        //    }
        //}
    }
    public void CookRecipe(int id)
    {
        //制作药方，默认一次只能制作一个
        //foreach (var item in recipeInventory)
        //{
        //    if (item.ID == id)
        //    {
        //        if (item.Quantity >= 1)
        //        {
        //            item.Quantity -= 1;
        //        }

        //    }
        //}
    }
    public void DeleteRecipe(int id)
    {
        ////删除药方
        foreach (var item in recipeInventory)
        {
            if (item.Id == id)
            {
                recipeInventory.Remove(item);
                break;
            }
        }
    }
    public List<RecipeItem> GetAllRecipeItems()
    {
        //获取所有药方
        return recipeInventory;
    }
    public RecipeItem GetRecipeItemByID(int id)
    {
        //根据id查找药材
        //1，2，3....
        foreach (var item in recipeInventory)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return null;
    }
}