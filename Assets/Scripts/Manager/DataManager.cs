using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : SingleMono<DataManager>
{
    public int Prestige { get; private set; } = 150; // 测试用

    public void ChangePrestige(int change) 
    {
        Prestige += change;
    }
}
