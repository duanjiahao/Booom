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
        
        CommonUtils.GetAttributeValueAndVisible(HerbList, attributes, visibleList);

        for (int i = 1; i <= 4; i++)
        {
            var con = CommonUtils.GetEffectAttributeConfig((EffectAttributeType)i, attributes[i - 1]);
            if (con != null) 
            {
                effectList.Add(new EffectInfoData(con, visibleList[i - 1]));
                //获取对应的负面效果
                EffectAxisConfig badeffect = CommonUtils.GetCorrespondBadEffectConfig(con);
                effectList.Add(new EffectInfoData(badeffect, visibleList[i - 1]));
            }
        }

        return effectList;
    }
}
