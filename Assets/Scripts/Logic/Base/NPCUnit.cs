using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCUnit
{
    // NPC的基础数据
    public NPCGroupsConfig _npcConfig;

    // NPC的需求对话数据（包含了需求的属性）
    public NPCNeedDialogueConfig _npcNeedDialogConfig;

    // NPC的禁忌对话数据（包含了禁忌的属性, 可能没有，为null）
    public NPCAvoidDialogueConfig _npcAvoidDialogConfig;

    // NPC的需求效果Id数组
    public List<int> _needEffectIds;

    // NPC的禁忌效果Id数组（可能没有禁忌，为null）
    public List<int> _avoidEffectIds;

    // NPC的姓名
    public string Name { get; private set; }

    // NPC的立绘资源地址
    public string ImgPath { get; private set; }

    // NPC的头像资源地址
    public string HeadImgPath { get; private set; }

    // NPC的声望数值
    public int NPCPrestige;

    public NPCUnit Init() 
    {
        // 各种从表里随机数据
        GenerateConfig();

        return this;
    }

    private void GenerateConfig()
    {
        // 筛选出符合声望解锁条件的config
        var npcGroupConfigList = ConfigManager.Instance.GetConfigListWithFilter<NPCGroupsConfig>((config) =>
        {
            return DataManager.Instance.Prestige >= config.unlockPrestige;
        });

        if (npcGroupConfigList == null || npcGroupConfigList.Count == 0)
        {
            Debug.LogError($"错误，没有筛选出一个NPC Group Config！当前声望:{DataManager.Instance.Prestige}");
            return;
        }

        // 根据权重得出最终的NPC Config

        var weights = new List<int>();
        foreach (var config in npcGroupConfigList)
        {
            weights.Add(config.weights);
        }
        _npcConfig = npcGroupConfigList[CommonUtils.RollRange(weights)[0]];

        var needId = _npcConfig.needGroups[Random.Range(0, _npcConfig.needGroups.Length)];

        _npcNeedDialogConfig = ConfigManager.Instance.GetConfig<NPCNeedDialogueConfig>(needId);

        // 设置需求EffectIds
        _needEffectIds = new List<int>();
        if (_npcNeedDialogConfig.needID1 > 0) 
        {
            _needEffectIds.Add(_npcNeedDialogConfig.needID1);
        }
        if (_npcNeedDialogConfig.needID2 > 0)
        {
            _needEffectIds.Add(_npcNeedDialogConfig.needID2);
        }
        if (_npcNeedDialogConfig.needID3 > 0)
        {
            _needEffectIds.Add(_npcNeedDialogConfig.needID3);
        }

        _npcAvoidDialogConfig = null;
        var needAttributes = new List<int>(_npcNeedDialogConfig.needAttributes);

        // 把禁忌Groups数组放到一个List里，这个List用于打乱随机
        var avoidCandidateList = new List<int>(_npcConfig.avoidGroups);

        while (avoidCandidateList.Count > 0) 
        {
            var index = Random.Range(0, avoidCandidateList.Count);
            var avoidCandidate = avoidCandidateList[index];
            avoidCandidateList.RemoveAt(index);

            var config = ConfigManager.Instance.GetConfig<NPCAvoidDialogueConfig>(avoidCandidate);

            var avoidAttributes = new List<int>(config.avoidAttributes);

            foreach (var avoidAttribute in avoidAttributes)
            {
                if (needAttributes.Contains(avoidAttribute)) 
                {
                    // 因为有重复的属性，所以接着找
                    continue;
                }
            }

            // 没有重复的属性，确定是这个禁忌效果
            _npcAvoidDialogConfig = config;
            break;
        }

        _avoidEffectIds = null;
        if (_npcAvoidDialogConfig != null) 
        {
            // 设置禁忌EffectIds
            _avoidEffectIds = new List<int>();
            if (_npcAvoidDialogConfig.avoidID1 > 0)
            {
                _avoidEffectIds.Add(_npcAvoidDialogConfig.avoidID1);
            }
            if (_npcAvoidDialogConfig.avoidID2 > 0)
            {
                _avoidEffectIds.Add(_npcAvoidDialogConfig.avoidID2);
            }
        }

        var nameConfigs = ConfigManager.Instance.GetConfigListWithFilter<NPCNameConfig>((config)=> 
        {
            return config.gender == _npcConfig.gender;
        });

        Name = nameConfigs[Random.Range(0, nameConfigs.Count - 1)].name;

        ImgPath = _npcConfig.imageGroups[Random.Range(0, _npcConfig.imageGroups.Length - 1)];
        HeadImgPath = _npcConfig.HeadGroups[Random.Range(0, _npcConfig.imageGroups.Length - 1)];

        //设置npc对应的声望数值
        NPCPrestige = _npcConfig.prestigeLevel[Random.Range(0, _npcConfig.prestigeLevel.Length - 1)];
    }
}
