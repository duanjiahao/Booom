using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class RecipeDataManager : SingleMono<RecipeDataManager>
{
    public Inventory<BackpackRecipeItem> recipeInventory = new Inventory<BackpackRecipeItem>();
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
                var obj = new BackpackRecipeItem();
                obj.InitItemInfo(item.id, item.name, item.desc, 0, new string[] { item.attribute1.ToString(), item.attribute2.ToString(), item.attribute3.ToString() });
                //将数据加入列表中
                recipeInventory.AddItem(obj);
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
        var tempItem = new BackpackRecipeItem();
        foreach(var item in recipeInventory.GetAllItems())
        {
            if(item.ID == id)
            {
                tempItem = item;
                Debug.Log("you have increased a recipe's quantity:" + recipeInventory.GetItem(id).Name);
            }
        }
        if (tempItem != null)
        {
            recipeInventory.AddItem(tempItem);
        }
        
    }
    public void UseRecipe(int id)
    {
        //使用药方，默认一次只能用一个
        recipeInventory.UseItem(id, 1);
        Debug.Log("you have created a recipe:" + recipeInventory.GetItem(id).Name);
    }
    public List<BackpackRecipeItem> GetAllRecipeItems()
    {
        //获取所有药方
        return recipeInventory.GetAllItems();
    }
    public BackpackRecipeItem GetRecipeItemByID(int id)
    {
        //根据id查找药材
        return recipeInventory.GetItem(id);
    }
}