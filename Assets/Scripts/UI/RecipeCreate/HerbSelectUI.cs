using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class HerbSelectUI : MonoBehaviour
{
    public TextMeshProUGUI num;

    public Transform itemContainer;

    public Button resetBtn;

    public Button createBtn;

    public Slider slider;

    public TextMeshProUGUI weight;

    public List<HerbWeightData> CurrentWeightDataList { get; private set; }
    public Dictionary<int, HerbWeightData> CurrentWeightDataDic { get; private set; }

    private List<HerbPickItem> UIItemList = new List<HerbPickItem>();

    private Dictionary<int, HerbPickItem> UIItemDic = new Dictionary<int, HerbPickItem>();

    private const int HerbMaxCount = 3;

    private void OnEnable()
    {
        createBtn.onClick.AddListener(OnCreateBtnClick);
        resetBtn.onClick.AddListener(onResetBtnClick);
    }

    private void onResetBtnClick()
    {
        foreach (var item in UIItemList)
        {
            GameObject.Destroy(item.gameObject);
        }
        UIItemList.Clear();
        UIItemDic.Clear();

        CurrentWeightDataDic.Clear();
        CurrentWeightDataList.Clear();
    }

    private void OnCreateBtnClick()
    {
    }

    private void OnDisable()
    {
        createBtn.onClick.RemoveListener(OnCreateBtnClick);
        resetBtn.onClick.RemoveListener(onResetBtnClick);
    }


    public void AddWeight(HerbWeightData data) 
    {
        bool contain = CurrentWeightDataDic.ContainsKey(data.HerbId);

        if (contain)
        {
            CurrentWeightDataDic[data.HerbId].Weight += data.Weight;
            RefreshUI(data.HerbId, CurrentWeightDataDic[data.HerbId].Weight, false);
        }
        else 
        {
            if (CurrentWeightDataList.Count == HerbMaxCount)
            {
                return;
            }

            CurrentWeightDataList.Add(data);
            CurrentWeightDataDic.Add(data.HerbId, data);
            RefreshUI(data.HerbId, data.Weight, true);
        }
    }

    private void RefreshUI(int herbId, int weight, bool newItem) 
    {
        if (newItem)
        {
            var item = Instantiate(Resources.Load<GameObject>("Prefab/HerbCreate/HerbPickItem"), itemContainer);
            var mono = item.GetComponent<HerbPickItem>();
            mono.SetData(new HerbWeightData { HerbId = herbId, Weight = weight });
            UIItemList.Add(mono);
            UIItemDic.Add(herbId, mono);

            num.text = $"| {CurrentWeightDataList.Count}/{HerbMaxCount}";
        }
        else 
        {
            UIItemDic[herbId].SetData(new HerbWeightData { HerbId = herbId, Weight = weight });
        }

        this.weight.text = $"{weight / 10}''{weight % 10}'";

        slider.value = Mathf.InverseLerp(0f, 100f, weight);
    }
}
