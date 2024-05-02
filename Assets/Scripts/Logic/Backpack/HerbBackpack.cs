using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using FindFunc;
//药材背包UI
public class HerbBackpack : MonoBehaviour
{
    //herb list manager
    public GameObject PagingRoot;
    public GameObject HerbPrefab;
    private GameObject tempItem;
    private List<HerbItem> herbInventory = new List<HerbItem>();
    private List<GameObject> itemList = new List<GameObject>();
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        //FindInfo();
    }
    void FindInfo()
    {
        //<组件类型>（根节点,查询名称）
        //组件类型：是非GameObject的其他组件
        //查询名称：最好同名，这样以后方便维护
        //_idText = UnityHelper.GetTheChildNodeComponetScripts<Text>(PagingRoot, "id");


    }
    // Start is called before the first frame update
    void Start()
    {
        herbInventory = HerbDataManager.Instance.GetAllHerbItems();
        //显示前五个
        SetNextItems(0, 5);

    }
    public void SetNextItems(int index, int size)
    {
        //向后翻页逻辑
        ClearItemList();
        int end = Mathf.Min(index + size, herbInventory.Count);
        CreateAndDisplayItems(index, end);
    }

    public void SetPrevItems(int index, int size)
    {
        //向前翻页逻辑
        ClearItemList();
        int end = Mathf.Min(index + size, herbInventory.Count);
        //int start = Mathf.Max(0, index - size);
        CreateAndDisplayItems(index, end);
    }

    private void ClearItemList()
    {
        //清理列表
        foreach (var item in itemList)
        {
            Destroy(item);
        }
        itemList.Clear();
    }

    private void CreateAndDisplayItems(int start, int end)
    {
        //根据起始和结束索引index显示item
        for (int i = start; i < end; i++)
        {
            HerbItem data = herbInventory[i];
            GameObject tempItem = Instantiate(
                HerbPrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
            tempItem.GetComponentInChildren<HerbUnitInfo>().SetData(data);
            itemList.Add(tempItem);
            Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "weight");
            nameText.text = data.Quantity.ToString();
        }
    }

}