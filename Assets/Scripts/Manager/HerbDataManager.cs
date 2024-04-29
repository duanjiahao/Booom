using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
public class HerbDataManager : SingleMono<HerbDataManager>
{
    // 创建药方背包
    public Inventory<BackpackHerbItem> herbInventory = new Inventory<BackpackHerbItem>();
    //药方管理
    public override void Init()
    {
        base.Init();
        
        // 从json中读取数据
        TextAsset configFile = Resources.Load<TextAsset>("Configs/HerbsConfig");
        string text = configFile.text;
        if (text != null)
        {
            //反序列化json
            var herbDict = JsonConvert.DeserializeObject<Dictionary<int, HerbsConfig>>(text);
            List<HerbsConfig> herbList = new List<HerbsConfig>(herbDict.Values);
            foreach (var item in herbList)
            {
                var obj = new BackpackHerbItem();
                obj.InitItemInfo(item.id, item.name, item.desc, 0, new int[] { item.attribute1, item.attribute2, item.attribute3, item.attribute4 },item.iconPath);
                //将数据加入列表中
                herbInventory.AddItem(obj);
                Debug.Log($"Item ID: {item.id}, Name: {item.name}, Description: {item.desc}");
            }

        }
        else
        {
            Debug.LogError("Failed to load the config file.");
        }
    }
    public void AddHerb(int id)
    {
        //添加药材
        var tempItem = new BackpackHerbItem();
        foreach (var item in herbInventory.GetAllItems())
        {
            if (item.ID == id)
            {
                tempItem = item;
            }
        }
        if (tempItem != null)
        {
            herbInventory.AddItem(tempItem);
        }

    }
    public void UseHerb(int id, int quantity)
    {
        //使用药材
        herbInventory.UseItem(id, quantity);
    }
    public List<BackpackHerbItem> GetAllHerbItems()
    {
        //获取所有药材
        return herbInventory.GetAllItems();
    }
    public BackpackHerbItem GetHerbItemByID(int id)
    {
        //根据id查找药材
        return herbInventory.GetItem(id);
    }

}
