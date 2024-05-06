using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CommonUtils
{
    public static List<int> RollRange(List<int> weights, int rollTime = 1) 
    {
        List<int> result = new List<int>();

        float amount = 0;
        foreach (var weight in weights)
        {
            amount += weight;
        }

        for (int i = 0; i < rollTime; i++)
        {
            var roll = Random.Range(0, amount);

            for (int j = 0; j < weights.Count; j++)
            {
                if (result.Contains(j)) continue;

                var weight = weights[j];
                if (weight >= roll) 
                {
                    result.Add(j);
                    amount -= weight;
                    break;
                }

                roll -= weight;
            }
        }

        return result;
    }

    public static PrestigeLevelConfig GetCurrentPrestigeConfig() 
    {
        return GetCurrentPrestigeConfigWithNext(out var _);
    }

    public static PrestigeLevelConfig GetCurrentPrestigeConfigWithNext(out PrestigeLevelConfig nextLevelConfig)
    {
        var configList = ConfigManager.Instance.GetConfigListWithFilter<PrestigeLevelConfig>((config) =>
        {
            return DataManager.Instance.Prestige >= config.lowerLimit;
        });

        // 最有一个config就是目前的声望等级
        var currentConfig = configList[configList.Count - 1];

        nextLevelConfig = ConfigManager.Instance.GetConfig<PrestigeLevelConfig>(currentConfig.id + 1);

        return currentConfig;
    }

    public static string GetWeightStr(int weight) 
    {
        if (weight < 10)
        {
            return $"{GetChineseNumber(weight)}钱";
        }
        else 
        {
            var ten = weight / 10;
            var one = weight % 10;
            if (ten <= 10)
            {
                if (one == 0)
                {
                    return $"{GetChineseNumber(ten)}两";
                }
                else
                {
                    return $"{GetChineseNumber(ten)}两{GetChineseNumber(one)}钱";
                }
            }
            else 
            {
                var hun = ten / 10;
                var left = ten % 10;
                if (hun == 1)
                {
                    if (one == 0)
                    {
                        return $"{GetChineseNumber(10)}{GetChineseNumber(left)}两";
                    }
                    else
                    {
                        return $"{GetChineseNumber(10)}{GetChineseNumber(left)}两{GetChineseNumber(one)}钱";
                    }
                }
                else 
                {
                    if (one == 0)
                    {
                        return $"{GetChineseNumber(hun)}十{GetChineseNumber(left)}两";
                    }
                    else
                    {
                        return $"{GetChineseNumber(hun)}十{GetChineseNumber(left)}两{GetChineseNumber(one)}钱";
                    }
                }
            }
        }
    }

    public static string GetChineseNumber(int digit) 
    {
        switch (digit)
        {
            case 0:
                return "零";
            case 1:
                return "一";
            case 2:
                return "二";
            case 3:
                return "三";
            case 4:
                return "四";
            case 5:
                return "五";
            case 6:
                return "六";
            case 7:
                return "七";
            case 8:
                return "八";
            case 9:
                return "九";
            case 10:
                return "十";
            default:
                return "？";
        }
    }

    public static EffectAxisConfig GetEffectAttributeConfig(EffectAttributeType type, int value) 
    {
        var effectAxisConfigList = ConfigManager.Instance.GetConfigListWithFilter<EffectAxisConfig>((con) =>
        {
            return con.attributes == (int)type && con.isPositive;
        }) as List<EffectAxisConfig>;

        effectAxisConfigList.Sort((EffectAxisConfig config, EffectAxisConfig other) => 
        {
            return config.value.CompareTo(other.value);
        });

        EffectAxisConfig EffectAxisConfig = null;
        for (int i = 0; i < effectAxisConfigList.Count; i++)
        {
            var effectValue = effectAxisConfigList[i].value;
            if (effectValue < 0 && value <= effectValue)
            {
                EffectAxisConfig = effectAxisConfigList[i];
                break;
            }
            else if (effectValue > 0)
            {
                if (value >= effectValue && (i == effectAxisConfigList.Count - 1 || value < effectAxisConfigList[i + 1].value))
                {
                    EffectAxisConfig = effectAxisConfigList[i];
                    break;
                }
            }
        }

        return EffectAxisConfig;
    }

    public static void DestroyAllChildren(this Transform tran)
    {
        for (int i = 0; i < tran.childCount; i++)
        {
            var child = tran.GetChild(i);
            Object.Destroy(child.gameObject);
        }
    }

    public static void GetAttributeValueAndVisible(List<HerbWeightData> herbList, int[] attributes, bool[] visibleList)
    {
        foreach (var herb in herbList)
        {
            var herbConfig = ConfigManager.Instance.GetConfig<HerbsConfig>(herb.HerbId);
            var herbItem = HerbDataManager.Instance.GetHerbItemByID(herb.HerbId);
            //计算四个象限的属性值
            attributes[0] += herbConfig.attribute1 * herb.Weight;
            attributes[1] += herbConfig.attribute2 * herb.Weight;
            attributes[2] += herbConfig.attribute3 * herb.Weight;
            attributes[3] += herbConfig.attribute4 * herb.Weight;
            
            var hasInvisible = herbItem.HasInvisibleAttribute();
            
            //效果可见性 (如果一个药材有不可见的属性，那么为0的属性也是不可见的)
            if ((hasInvisible && herbConfig.attribute1 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Yang))
            {
                visibleList[0] = false;
            }
            if ((hasInvisible && herbConfig.attribute2 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Yin))
            {
                visibleList[1] = false;
            }
            if ((hasInvisible && herbConfig.attribute3 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Re))
            {
                visibleList[2] = false;
            }
            if ((hasInvisible && herbConfig.attribute4 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Han))
            {
                visibleList[3] = false;
            }
        }
    }
    
    public static void GetAttributeValueAndVisible(List<HerbRecipeInfo> herbList, int[] attributes, bool[] visibleList)
    {
        foreach (var herb in herbList)
        {
            var herbConfig = ConfigManager.Instance.GetConfig<HerbsConfig>(herb.HerbId);
            var herbItem = HerbDataManager.Instance.GetHerbItemByID(herb.HerbId);
            //计算四个象限的属性值
            attributes[0] += herbConfig.attribute1 * herb.Weight;
            attributes[1] += herbConfig.attribute2 * herb.Weight;
            attributes[2] += herbConfig.attribute3 * herb.Weight;
            attributes[3] += herbConfig.attribute4 * herb.Weight;

            var hasInvisible = herbItem.HasInvisibleAttribute();
            
            //效果可见性 (如果一个药材有不可见的属性，那么为0的属性也是不可见的)
            if ((hasInvisible && herbConfig.attribute1 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Yang))
            {
                visibleList[0] = false;
            }
            if ((hasInvisible && herbConfig.attribute2 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Yin))
            {
                visibleList[1] = false;
            }
            if ((hasInvisible && herbConfig.attribute3 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Re))
            {
                visibleList[2] = false;
            }
            if ((hasInvisible && herbConfig.attribute4 == 0) || !herbItem.IsAttributeVisible(EffectAttributeType.Han))
            {
                visibleList[3] = false;
            }
        }
    }

    public static EffectAxisConfig GetCorrespondBadEffectConfig(EffectAxisConfig goodEffect)
    {
        var badEffectConfigList = ConfigManager.Instance.GetConfigListWithFilter<EffectAxisConfig>((config) =>
        {
            return !config.isPositive && config.value == goodEffect.value;
        });
        return badEffectConfigList[0];
    }

    public static bool UseHerbCreateRecipe(RecipeItem recipeItem, int num)
    {
        if (!CheckBagEnough(recipeItem.HerbList, num))
        {
            return false;
        }

        for (int i = 0; i < recipeItem.HerbList?.Count; i++)
        {
            var herb = recipeItem.HerbList[i];
            HerbDataManager.Instance.UseHerb(herb.HerbId, herb.Weight * num);
        }
        
        RecipeDataManager.Instance.CookRecipe(recipeItem.Id, num);
        return true;
    }

    public static bool CheckBagEnough(List<HerbRecipeInfo> herbList, int num)
    {
        for (int i = 0; i < herbList?.Count; i++)
        {
            var herb = herbList[i];
            var has = HerbDataManager.Instance.GetHerbItemByID(herb.HerbId)?.Quantity ?? 0;
            if (has < herb.Weight * num)
            {
                return false;
            }
        }

        return true;
    }

    public static void ShowTips(Vector2 pos, string content)
    {
        var go = Object.Instantiate(Resources.Load<GameObject>("UI/Tips"), GameObject.Find("Canvas").transform);
        go.transform.SetAsLastSibling();
        go.GetComponent<RectTransform>().anchoredPosition = pos;
        go.GetComponent<Tips>().SetContent(content);
    }
}
