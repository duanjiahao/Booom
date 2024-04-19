using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HerbData
{
    public int herbID;
    public string name;
    public string desc;
    //是否已解锁
    public bool isUnlock;
    public int unlockDate;
    #region 药材属性及其可见性
    public int attribute1;
    public bool isAttribute1visible;
    public int attribute2;
    public bool isAttribute2visible;
    public int attribute3;
    public bool isAttribute3visible;
    public int attribute4;
    public bool isAttribute4visible;
    #endregion
    public int collectionTimePeriod;
    public int[] collectionPrestige;
    public int collectionWeights;
    public int[] rewardWeight;
    public string iconPath;
    public string heapPath1;
    public string heapPath2;
    public string heapPath3;
}

public class HerbDataList
{
    private List<HerbData> herbDataList = new List<HerbData>();

    private void InitHerbDataList()
    {
        //TODO
    }
    public List<HerbData> GetAllHerbData()
    {
        return herbDataList;
    }

    public HerbData GetHerbData(int id)
    {
        HerbData data = new HerbData();
        //TODO
        return data;
    }

    public void AddData(HerbData data)
    {
        //TODO
        herbDataList.Add(data);
    }

    public void DeleteData(HerbData data)
    {
        //TODO
        herbDataList.Remove(data);
    }
}