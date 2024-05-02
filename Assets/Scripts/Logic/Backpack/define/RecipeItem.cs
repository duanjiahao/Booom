using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//药效类
public class EffectInfoData
{
    public EffectAxisConfig EffectAxisConfig;
    public bool IsVisible;

    public EffectInfoData(EffectAxisConfig EffectInfo, bool IsVisible)
    {
        this.EffectAxisConfig = EffectInfo;
        this.IsVisible = IsVisible;
    }
}

public class HerbRecipeInfo 
{
    public int HerbId;

    public int Weight;
}

//药方动态数据
public class RecipeItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    
    public int Num { get; set; }
    public List<HerbRecipeInfo> HerbList { get; set; }

    public RecipeItem(int id, string name, int number, List<HerbRecipeInfo> herbList) 
    {
        Id = id;
        Name = name;
        Num = number;
        HerbList = herbList;
    }

    public List<EffectInfoData> GetEffectList()
    {
        //获取药方的效果列表
        List<EffectInfoData> effectList = new List<EffectInfoData>();
        bool[] visibleList = { true,true,true,true };
        int[] attributes = { 0, 0, 0, 0 };
        foreach (var herb in HerbList)
        {
            var herbConfig = ConfigManager.Instance.GetConfig<HerbsConfig>(herb.HerbId);
            var herbItem = HerbDataManager.Instance.GetHerbItemByID(herb.HerbId);
            //计算四个象限的属性值
            attributes[0] += herbConfig.attribute1;
            attributes[1] += herbConfig.attribute2;
            attributes[2] += herbConfig.attribute3;
            attributes[3] += herbConfig.attribute4;
            //效果可见性
            if (!herbItem.IsAttributeVisible(EffectAttributeType.Yang))
            {
                visibleList[0] = false;
            }
            if (!herbItem.IsAttributeVisible(EffectAttributeType.Yin))
            {
                visibleList[1] = false;
            }
            if (!herbItem.IsAttributeVisible(EffectAttributeType.Re))
            {
                visibleList[2] = false;
            }
            if (!herbItem.IsAttributeVisible(EffectAttributeType.Han))
            {
                visibleList[3] = false;
            }
        }

        for (int i = 1; i <= 4; i++)
        {
            var con = CommonUtils.GetEffectAttributeConfig((EffectAttributeType)i, attributes[i - 1]);
            if (con != null) 
            {
                effectList.Add(new EffectInfoData(con, visibleList[i - 1]));
            }
        }

        return effectList;
    }
}
