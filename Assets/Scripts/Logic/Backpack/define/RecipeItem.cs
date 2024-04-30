using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//药效类
public class EffectItem
{
    public EffectAxisConfig EffectInfo;
    public bool IsVisible;

    public EffectItem(EffectAxisConfig EffectInfo, bool IsVisible)
    {
        this.EffectInfo = EffectInfo;
        this.IsVisible = IsVisible;
    }
}
//药方动态数据
public class RecipeItem
{
    public int ID { get; set; }
    public string Name { get; set; }
    
    public int Quantity { get; set; }
    public List<HerbItem> HerbList { get; set; }
    public List<EffectItem> EffectList { get; set; }

    public void InitItemInfo(int id, string name, int quantity, List<HerbItem> herbList, List<EffectAxisConfig> AllEffectList)
    {
        this.ID = id;
        this.Name = name;
        this.Quantity = quantity;
        this.HerbList = herbList;
        this.EffectList = GetEffectList(herbList,AllEffectList);

    }
    private List<EffectItem> GetEffectList(List<HerbItem> herbList,List<EffectAxisConfig> allEffects)
    {
        //获取药方的效果列表
        List<EffectItem> effectList = new List<EffectItem>();
        bool[] visibleList = { true,true,true,true };
        int[] attributes = { 0, 0, 0, 0 }; // 假设attribute的索引与effect.attributes的值对应
        foreach (var herb in herbList)
        {
            //计算四个象限的属性值
            attributes[0] += herb.HerbConfig.attribute1;
            attributes[1] += herb.HerbConfig.attribute2;
            attributes[2] += herb.HerbConfig.attribute3;
            attributes[3] += herb.HerbConfig.attribute4;
            //效果可见性
            if (!herb.HerbConfig.isAttribute1visible)
            {
                visibleList[0] = false;
            }
            if (!herb.HerbConfig.isAttribute2visible)
            {
                visibleList[1] = false;
            }
            if (!herb.HerbConfig.isAttribute3visible)
            {
                visibleList[2] = false;
            }
            if (!herb.HerbConfig.isAttribute4visible)
            {
                visibleList[3] = false;
            }
        }
        foreach (var effect in allEffects)
        {
            //和效果表做对比，找到所属的效果并添加进药方的效果表里
            if (effect.attributes >= 1 && effect.attributes <= 4) // 确保effect.attributes在有效范围内
            {
                int relevantAttribute = attributes[effect.attributes - 1]; // 从数组中获取对应的属性值
                bool shouldAdd = (effect.value >= 0 && relevantAttribute >= effect.value) ||
                                 (effect.value < 0 && relevantAttribute <= effect.value);

                if (shouldAdd)
                {
                    effectList.Add(new EffectItem(effect, visibleList[effect.attributes -1]));
                }
            }
        }
        return effectList;
    }
}
