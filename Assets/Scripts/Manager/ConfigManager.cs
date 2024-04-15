using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class ConfigManager : SingleMono<ConfigManager>
{
    private Dictionary<string, Dictionary<int, BaseConfig>> ConfigDatas;

    public override void Init()
    {
        ConfigDatas = new Dictionary<string, Dictionary<int, BaseConfig>>();

        var textAssets = Resources.LoadAll<TextAsset>("Configs");
        for (int i = 0; i < textAssets?.Length; i++)
        {
            var textAsset = textAssets[i];
            var data = JsonConvert.DeserializeObject<Dictionary<int, BaseConfig>>(textAsset.text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            });
            ConfigDatas.Add(textAsset.name, data);
        }
    }

    public override void Begin()
    {
        var config = ConfigManager.Instance.GetConfig<ItemConfig>(1);
    }

    public T GetConfig<T>(int id) where T : BaseConfig
    {
        var configName = typeof(T).Name;
        if (ConfigDatas.ContainsKey(configName)) 
        {
            var data = ConfigDatas[configName];
            if (data.ContainsKey(id)) 
            {
                return data[id] as T;
            }
        }

        return null;
    }
}
