using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
//药材数据管理单例
public class HerbDataManager : Singleton<HerbDataManager>
{
    // 创建药材列表
    public List<HerbItem> herbInventory = new List<HerbItem>();
    protected override void Init()
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
                var obj = new HerbItem();
                //测试数据
                obj.InitItemInfo(item,23);
                //将数据加入列表中
                herbInventory.Add(obj);
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
        var tempItem = new HerbItem();
        foreach (var item in GetAllHerbItems())
        {
            if (item.HerbConfig.id == id)
            {
                tempItem = item;
            }
        }
        if (tempItem != null)
        {
            herbInventory.Add(tempItem);
        }

    }
    public void UseHerb(int id, int quantity)
    {
        //使用药材
        foreach (var item in herbInventory)
        {
            if (item.HerbConfig.id == id)
            {
                if (item.Quantity >= quantity)
                {
                    item.Quantity -= quantity;
                }
            }
        }
    }
    public List<HerbItem> GetAllHerbItems()
    {
        //获取所有药材
        return herbInventory;
    }
    public HerbItem GetHerbItemByID(int id)
    {
        //根据id查找药材
        foreach(var item in herbInventory)
        {
            if (item.HerbConfig.id == id)
            {
                return item;
            }
        }
        return null;
    }

}
