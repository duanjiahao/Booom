using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectUnit
{
    // 药效基本信息
    private EffectAxisConfig _effectConfig;


    public EffectUnit Init()
    {
        // 各种从表里随机数据
        GenerateConfig();

        return this;
    }

    private void GenerateConfig()
    {

    }
}
