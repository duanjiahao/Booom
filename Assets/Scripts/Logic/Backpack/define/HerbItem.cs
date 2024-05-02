using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//药材动态数据类
public class HerbItem
{
    public HerbsConfig HerbConfig { get; set; }
    //单位为1钱，10钱为1两（如2两6钱为26）
    public int Quantity { get; set; }
    public int[] AttributeList { get; set; }
    public bool[] IsVisible { get; set; }

    public HerbItem(int herbId, int quantity) 
    {
        this.HerbConfig = ConfigManager.Instance.GetConfig<HerbsConfig>(herbId);
        this.Quantity = quantity;
        this.AttributeList = GetAttribute();
        this.IsVisible = GetVisible();
    }

    private int[] GetAttribute()
    {
        int[] tempList = { HerbConfig.attribute1, HerbConfig.attribute2,
            HerbConfig.attribute3, HerbConfig.attribute4 };

        return tempList;
    }
    private bool[] GetVisible()
    {
        bool[] tempList = { HerbConfig.isAttribute1visible, HerbConfig.isAttribute2visible,
            HerbConfig.isAttribute3visible, HerbConfig.isAttribute4visible };

        return tempList;
    }

    public bool IsAttributeVisible(EffectAttributeType effectAttributeType) 
    {
        return IsVisible[(int)effectAttributeType - 1];
    }
}
