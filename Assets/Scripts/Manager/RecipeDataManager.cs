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
        base.Init();
        //读取数据
        TextAsset effectConfigFile = Resources.Load<TextAsset>("Configs/EffectAxisConfig");
        string effectText = effectConfigFile.text;
        if (effectText != null)
        {
            //反序列化json
            var effectDict = JsonConvert.DeserializeObject<Dictionary<int, EffectAxisConfig>>(effectText);
            effectInventory = new List<EffectAxisConfig>(effectDict.Values);
//            Debug.Log(effectInventory);
            //List<EffectAxisConfig> effectList = new List<EffectAxisConfig>(effectDict.Values);
        }
        #region 测试
        List<HerbItem> herbList = new List<HerbItem>();
        herbList.Add(HerbDataManager.Instance.GetHerbItemByID(1001));
        herbList.Add(HerbDataManager.Instance.GetHerbItemByID(1002));
        RecipeItem recipeItem = new RecipeItem();
        recipeItem.InitItemInfo(1, "first recipe", 12, herbList, effectInventory);
        recipeInventory.Add(recipeItem);
        RecipeItem recipeItem2 = new RecipeItem();
        recipeItem2.InitItemInfo(2, "second recipe", 32, herbList, effectInventory);
        recipeInventory.Add(recipeItem2);
        #endregion

    }

    public void AddRecipe(int id)
    {
        //添加药方
        var tempItem = new RecipeItem();
        foreach(var item in GetAllRecipeItems())
        {
            if(item.ID == id)
            {
                tempItem = item;
                Debug.Log("you have increased a recipe's quantity:" + GetRecipeItemByID(id).Name);
            }
        }
        if (tempItem != null)
        {
            recipeInventory.Add(tempItem);
        }
        
    }
    public void UseRecipe(int id)
    {
        //给出药方，默认一次只能用一个
        //根据id查找
        foreach (var item in recipeInventory)
        {
            if (item.ID == id)
            {
                if (item.Quantity >= 1)
                {
                    item.Quantity -= 1;
                }
                
            }
        }
    }
    public void CookRecipe(int id)
    {
        //制作药方，默认一次只能制作一个
        foreach (var item in recipeInventory)
        {
            if (item.ID == id)
            {
                if (item.Quantity >= 1)
                {
                    item.Quantity -= 1;
                }

            }
        }
    }
    public void DeleteRecipe(int id)
    {
        //删除药方
        foreach (var item in recipeInventory)
        {
            if (item.ID == id)
            {
                recipeInventory.Remove(item);
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
        foreach(var item in recipeInventory)
        {
            if (item.ID == id)
            {
                return item;
            }
        }
        return null;
    }
}