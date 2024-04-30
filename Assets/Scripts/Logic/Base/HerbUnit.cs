using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbUnit
{
    // 药材基本信息
    private HerbsConfig _herbConfig;

    // 药材贴图资源地址
    public string ImgPath { get; private set; }

    public HerbUnit Init()
    {
        // 各种从表里随机数据
        GenerateConfig();

        return this;
    }

    private void GenerateConfig()
    {

    }
}
