using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
//背包UI切换管理
public class BackpackPanelControl : MonoBehaviour
{
    public GameObject PagingRoot;
    //private Button BtRecipe;
    //private Button BtHerb;
    private GameObject recipePanel;
    private GameObject herbPanel;
    private Toggle TgHerb;
    private Toggle TgRecipe;
    private Toggle TgPrev;
    private Toggle TgNext;
    //当前的物体索引
    private int currentRecipeIndex = 0;
    private int currentHerbIndex = 0;
    //一次翻页的物体数量
    private int itemSize = 5;
    private List<GameObject> itemList = new List<GameObject>();
    public GameObject RecipePrefab;
    public GameObject HerbPrefab;
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    private void OnEnable()
    {
        UpdateToggleStates();
        RefreshBackpackData();
    }
    void FindInfo()
    {
        //<组件类型>（根节点,查询名称）
        //组件类型：是非GameObject的其他组件
        //查询名称：最好同名，这样以后方便维护
        TgPrev = UnityHelper.GetTheChildNodeComponetScripts<Toggle>(PagingRoot, "TgPrev");
        TgNext = UnityHelper.GetTheChildNodeComponetScripts<Toggle>(PagingRoot, "TgNext");
        TgHerb = UnityHelper.GetTheChildNodeComponetScripts<Toggle>(PagingRoot, "TgHerb");
        TgRecipe = UnityHelper.GetTheChildNodeComponetScripts<Toggle>(PagingRoot, "TgRecipe");

    }
    // Start is called before the first frame update
    void Start()
    {
        recipePanel = transform.Find("recipeList").gameObject;
        herbPanel = transform.Find("herbList").gameObject;

        // 注册Toggle的ValueChanged事件
        TgRecipe.isOn = true;
        TgHerb.onValueChanged.AddListener(OnToggleHerbsChanged);
        TgRecipe.onValueChanged.AddListener(OnToggleRecipeChanged);

        // 初始化时更新UI状态
        UpdateVisibility();
    }
    public void RefreshBackpackData()
    {
        // 每次刷新时重置到第一页
        currentHerbIndex = 0;
        currentRecipeIndex = 0;
        TgRecipe.isOn = true;
        UpdateToggleStates();
        DisplayHerbItems(currentHerbIndex);
        DisplayRecipeItems(currentRecipeIndex);
        
    }
    public void MoveLeft()
    {
        if (TgRecipe.isOn)
        {
            // 向前翻页逻辑
            if (currentRecipeIndex - itemSize >= 0)
            {
                currentRecipeIndex -= itemSize;
                DisplayRecipeItems(currentRecipeIndex);
                
            }
            UpdateToggleStates();
        }
        else
        {
            if (currentHerbIndex - itemSize >= 0)
            {
                currentHerbIndex -= itemSize;
                DisplayHerbItems(currentHerbIndex);

            }
            UpdateToggleStates();
        }
    }

    public void MoveRight()
    {
        if (TgHerb.isOn)
        {
            // 向后翻页逻辑
            if (currentHerbIndex + itemSize < HerbDataManager.Instance.GetAllHerbItems().Count)
            {
                currentHerbIndex += itemSize;
                DisplayHerbItems(currentHerbIndex);
                
            }
            UpdateToggleStates();
        }
        else
        {
            if (currentRecipeIndex + itemSize < RecipeDataManager.Instance.GetAllRecipeItems().Count)
            {
                currentRecipeIndex += itemSize;
                DisplayRecipeItems(currentRecipeIndex);

            }
            UpdateToggleStates();
        }
    }

    void OnToggleRecipeChanged(bool isOn)
    {
        // 当药方列表的Toggle变化时调用
        if (isOn)
        {
            //transform.Find("recipeList").GetComponent<RecipeBackpack>().SetNextItems(currentRecipeIndex, itemSize);
            DisplayRecipeItems(currentRecipeIndex);
            UpdateVisibility();
            UpdateToggleStates();
        }
        
    }

    void OnToggleHerbsChanged(bool isOn)
    {
        // 当药材列表的Toggle变化时调用
        if (isOn)
        {
            //transform.Find("herbList").GetComponent<HerbBackpack>().SetNextItems(currentHerbIndex, itemSize);
            DisplayHerbItems(currentHerbIndex);
            UpdateVisibility();
            UpdateToggleStates();
        }
    }

    void UpdateVisibility()
    {
        // 根据Toggle的状态显示或隐藏对应的Panel
        recipePanel.SetActive(TgRecipe.isOn);
        herbPanel.SetActive(TgHerb.isOn);
    }

    public void ClearItemList()
    {
        //清理列表
        foreach (var item in itemList)
        {
            Destroy(item);
        }
        itemList.Clear();
    }

    public void DisplayRecipeItems(int start)
    {
        ClearItemList();
        List<RecipeItem> recipeInventory = RecipeDataManager.Instance.GetAllRecipeItems();
        int end = Mathf.Min(start + itemSize, recipeInventory.Count);
        recipeInventory = RecipeDataManager.Instance.GetAllRecipeItems();
        //根据起始和结束索引index显示item
        for (int i = start; i < end; i++)
        {
            RecipeItem data = recipeInventory[i];
            GameObject tempItem = Instantiate(
                RecipePrefab,
                transform.position,
                Quaternion.identity,
                transform.Find("recipeList")
            );
            tempItem.GetComponentInChildren<RecipeUnitInfo>().SetData(data);
            itemList.Add(tempItem);
            Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "name");
            nameText.text = data.Name;
            Text quantityText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "quantity");
            quantityText.text = "x " + data.Num.ToString();
        }
    }
    public void DisplayHerbItems(int start)
    {
        ClearItemList();
        List<HerbItem> herbInventory = HerbDataManager.Instance.GetAllHerbItems();
        int end = Mathf.Min(start + 5, herbInventory.Count);
        herbInventory = HerbDataManager.Instance.GetAllHerbItems();
        //根据起始和结束索引index显示item
        for (int i = start; i < end; i++)
        {
            HerbItem data = herbInventory[i];
            GameObject tempItem = Instantiate(
                HerbPrefab,
                transform.position,
                Quaternion.identity,
                transform.Find("herbList")
            );
            tempItem.GetComponentInChildren<HerbUnitInfo>().SetData(data);
            itemList.Add(tempItem);
            Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "weight");
            nameText.text = CommonUtils.GetWeightStr(data.Quantity);
            Image HerbImg = UnityHelper.GetTheChildNodeComponetScripts<Image>(tempItem, "herbImg");
            HerbImg.sprite = Resources.Load<Sprite>(data.HerbConfig.iconPath);
        }
    }
    void UpdateToggleStates()
    {
        if (TgHerb.isOn)
        {
            TgPrev.interactable = currentHerbIndex > 0;
            TgNext.interactable = currentHerbIndex + itemSize < HerbDataManager.Instance.GetAllHerbItems().Count;
        }
        else
        {
            TgPrev.interactable = currentRecipeIndex > 0;
            TgNext.interactable = currentRecipeIndex + itemSize < RecipeDataManager.Instance.GetAllRecipeItems().Count;
        }

    }
    public void RefreshRecipe()
    {
        DisplayRecipeItems(currentRecipeIndex);
    }
    public void RefreshHerb()
    {
        DisplayHerbItems(currentHerbIndex);
    }
}
