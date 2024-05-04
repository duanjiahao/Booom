using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICollectionWindow : MonoBehaviour
{
    public Transform container;

    public HerbUIItem prefab;

    public Button btn;

    private void OnEnable()
    {
        RefreshUI();
        btn.onClick.AddListener(OnBtnClicked);
    }

    private void OnBtnClicked()
    {
        this.gameObject.SetActive(false);
        UIManager.Instance.OpenBackyardWindow();
    }

    private void OnDisable()
    {
        btn.onClick.RemoveListener(OnBtnClicked);
    }

    private void RefreshUI()
    {
        var herbItems = HerbPickerFactory.GetHerbPicker().GoPicking();

        var fakeHerbItemList = new List<HerbItem>();

        for (int i = 0; i < herbItems?.Count; i++)
        {
            var item = herbItems[i];
            
            HerbDataManager.Instance.AddHerb(item.ConfigId, item.Weight);

            var realHerbItem = HerbDataManager.Instance.GetHerbItemByID(item.ConfigId);

            var fakeHerbItem = new HerbItem(item.ConfigId, item.Weight);
            
            fakeHerbItem.IsVisible = realHerbItem.IsVisible;
            
            fakeHerbItemList.Add(fakeHerbItem);
        }
        
        container.DestroyAllChildren();

        for (int i = 0; i < fakeHerbItemList.Count; i++)
        {
            var fakeHerbItem = fakeHerbItemList[i];

            var go = GameObject.Instantiate(prefab.gameObject, container);
            go.SetActive(true);
            
            go.GetComponent<HerbUIItem>().InitUI(fakeHerbItem);
        }
    }
}
