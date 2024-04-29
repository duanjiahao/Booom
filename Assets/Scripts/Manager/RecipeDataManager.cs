using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class RecipeDataManager : SingleMono<RecipeDataManager>
{
    public List<RecipeItem> recipeInventory = new List<RecipeItem>();
    public override void Init()
    {
        base.Init();
        //读取数据
        TextAsset configFile = Resources.Load<TextAsset>("Configs/HerbsConfig");
        string text = configFile.text;
        if (text != null)
        {
            //反序列化json
            var herbDict = JsonConvert.DeserializeObject<Dictionary<int, HerbsConfig>>(text);
            List<HerbsConfig> herbList = new List<HerbsConfig>(herbDict.Values);
            foreach (var item in herbList)
            {
                var obj = new RecipeItem();
                obj.InitItemInfo(item.id, item.name, item.desc, 0, new string[] { item.attribute1.ToString(), item.attribute2.ToString(), item.attribute3.ToString() });
                //将数据加入列表中
                recipeInventory.Add(obj);
                //Debug.Log($"Item ID: {item.id}, Name: {item.name}, Description: {item.desc}");
            }

        }
        else
        {
            Debug.LogError("Failed to load the config file.");
        }
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
        //使用药方，默认一次只能用一个
        //根据id查找药材
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