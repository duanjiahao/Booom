using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbPicker : IHerbPicker
{
    private bool _isFreeTimeUsed;

    public void Init() 
    {
        Notification.Instance.Register(Notification.NextDay, OnRefreshFreeTime);
    }

    private void OnRefreshFreeTime(object data)
    {
        _isFreeTimeUsed = false;
    }

    public bool CanPick()
    {
        if (!_isFreeTimeUsed)
        {
            return true;
        }
        else
        {
            var config = ConfigManager.Instance.GetConfig<GeneralSettingsConfig>(1);
            return DataManager.Instance.Prestige >= config.prestigeCost;
        }
    }

    public IList<HerbRawItem> GoPicking()
    {
        // 扣声望
        if (_isFreeTimeUsed)
        {
            var config = ConfigManager.Instance.GetConfig<GeneralSettingsConfig>(1);
            DataManager.Instance.ChangePrestige(-config.prestigeCost);
        }
        else 
        {
            _isFreeTimeUsed = true;
        }

        var levelConfig = CommonUtils.GetCurrentPrestigeConfig();

        var index = UnityEngine.Random.Range(0, levelConfig.herbsTypeCount.Length);

        // 采集到的药材种类数
        var herbTypeCount = levelConfig.herbsTypeCount[index];

        var canPickHerbList = ConfigManager.Instance.GetConfigListWithFilter<HerbsConfig>((config)=> 
        {
            var collectionTimeList = new List<int>(config.collectionTime);
            return DataManager.Instance.Day >= config.unlockDate 
            && DataManager.Instance.Prestige >= config.collectionPrestige 
            && collectionTimeList.Contains((int)DataManager.Instance.CurrentTime);
        });

        List<HerbRawItem> HerbRawItemList = new List<HerbRawItem>();
        if (canPickHerbList.Count <= herbTypeCount)  // 不用随机了
        {
            foreach (var item in canPickHerbList)
            {
                HerbRawItemList.Add(new HerbRawItem()
                {
                    ConfigId = item.id,
                    Weight = UnityEngine.Random.Range(item.rewardWeight[0], item.rewardWeight[1] + 1),
                });
            }
        }
        else 
        {
            var weights = new List<int>();
            foreach (var canPickHerb in canPickHerbList)
            {
                weights.Add(canPickHerb.collectionWeights);
            }

            var indexList = CommonUtils.RollRange(weights, herbTypeCount);

            foreach (var herbIndex in indexList)
            {
                var item = canPickHerbList[herbIndex];

                HerbRawItemList.Add(new HerbRawItem()
                {
                    ConfigId = item.id,
                    Weight = UnityEngine.Random.Range(item.rewardWeight[0], item.rewardWeight[1] + 1),
                });
            }
        }

        // 时间往下走
        DataManager.Instance.MoveToNextTime();

        return HerbRawItemList;
    }

    public bool IsFreeThisTime()
    {
        return !_isFreeTimeUsed;
    }
}
