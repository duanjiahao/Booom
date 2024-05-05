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

    public List<HerbWeightData> CurrentWeightDataList { get; private set; } = new List<HerbWeightData>();

    public Dictionary<int, HerbWeightData> CurrentWeightDataDic { get; private set; } =
        new Dictionary<int, HerbWeightData>();

    private List<HerbPickItem> UIItemList = new List<HerbPickItem>();

    private Dictionary<int, HerbPickItem> UIItemDic = new Dictionary<int, HerbPickItem>();

    private const int HerbMaxCount = 3;

    private void OnEnable()
    {
        createBtn.onClick.AddListener(OnCreateBtnClick);
        resetBtn.onClick.AddListener(OnResetBtnClick);
    }

    private void OnResetBtnClick()
    {
        InitUI();
    }

    private void OnCreateBtnClick()
    {
        if (CurrentWeightDataList.Count > 0)
        {
            UIManager.Instance.OpenCreateRecipeWindow(CurrentWeightDataList);
        }
    }

    private void OnDisable()
    {
        createBtn.onClick.RemoveListener(OnCreateBtnClick);
        resetBtn.onClick.RemoveListener(OnResetBtnClick);
    }

    public void InitUI()
    {
        itemContainer.DestroyAllChildren();
        slider.value = 0f;
        this.weight.text = "零钱";
        num.text = $"| 0/{HerbMaxCount}";
        
        UIItemList.Clear();
        UIItemDic.Clear();
        CurrentWeightDataList.Clear();
        CurrentWeightDataDic.Clear();
    }

    public void AddWeight(HerbWeightData data) 
    {
        bool contain = CurrentWeightDataDic.ContainsKey(data.HerbId);

        if (contain)
        {
            CurrentWeightDataDic[data.HerbId].Weight += data.Weight;

            var maxHave = HerbDataManager.Instance.GetHerbItemByID(data.HerbId).Quantity;
            CurrentWeightDataDic[data.HerbId].Weight = Mathf.Min(CurrentWeightDataDic[data.HerbId].Weight, maxHave);
            
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

        this.weight.text = CommonUtils.GetWeightStr(weight);

        slider.value = Mathf.InverseLerp(0f, 140f, weight);
    }
}
