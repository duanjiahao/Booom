using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using FindFunc;
//药方背包UI
public class RecipeBackpack : MonoBehaviour
{
    public GameObject PagingRoot;
    public GameObject RecipePrefab;
    private GameObject tempItem;
    private List<GameObject> itemList = new List<GameObject>();
    private List<RecipeItem> recipeInventory = new List<RecipeItem>();
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
        recipeInventory = RecipeDataManager.Instance.GetAllRecipeItems();
        SetNextItems(0, 5);


    }
    public void SetNextItems(int index, int size)
    {
        //向后翻页逻辑
        ClearItemList();
        int end = Mathf.Min(index + size, recipeInventory.Count);
        CreateAndDisplayItems(index, end);
    }

    public void SetPrevItems(int index, int size)
    {
        //向前翻页逻辑
        ClearItemList();
        int end = Mathf.Min(index + size, recipeInventory.Count);
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
        recipeInventory = RecipeDataManager.Instance.GetAllRecipeItems();
        Debug.Log("recipe number is:"+recipeInventory.Count);
        //根据起始和结束索引index显示item
        for (int i = start; i < end; i++)
        {
            RecipeItem data = recipeInventory[i];
            GameObject tempItem = Instantiate(
                RecipePrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
            tempItem.GetComponentInChildren<RecipeUnitInfo>().SetData(data);
            itemList.Add(tempItem);
            Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "name");
            nameText.text = data.Name;
        }
    }
}
