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
        var settings = ConfigManager.Instance.GetConfig<GeneralSettingsConfig>(1);

        for (int i = 0; i < settings.initialHerbs?.Length; i++)
        {
            var herbId = settings.initialHerbs[i];
            var num = settings.initialHerbsWeight[i];
            var herbItem = new HerbItem(herbId, num);
            herbInventory.Add(herbItem);
        }
    }
    public void AddHerb(int id, int quantity)
    {
        ////添加药材
        foreach (var item in GetAllHerbItems())
        {
            if (item.HerbConfig.id == id)
            {
                item.Quantity += quantity;
                return;
            }
        }
        
        herbInventory.Add(new HerbItem(id, quantity));
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
