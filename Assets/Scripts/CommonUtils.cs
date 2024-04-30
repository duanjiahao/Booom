using System.Collections;
using System.Collections.Generic;
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
}
