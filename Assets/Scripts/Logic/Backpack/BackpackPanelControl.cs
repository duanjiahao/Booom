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
    private Button BtPrev;
    private Button BtNext;
    //当前的物体索引
    private int currentRecipeIndex = 0;
    private int currentHerbIndex = 0;
    //一次翻页的物体数量
    private int itemSize = 5;
    private void Awake()
    {
        //在Awake中需要赋值，也可以是别的根节点
        PagingRoot = this.gameObject;

        FindInfo();
    }
    void FindInfo()
    {
        //<组件类型>（根节点,查询名称）
        //组件类型：是非GameObject的其他组件
        //查询名称：最好同名，这样以后方便维护
        BtPrev = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtPrev");
        BtNext = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtNext");
        TgHerb = UnityHelper.GetTheChildNodeComponetScripts<Toggle>(PagingRoot, "TgHerb");
        TgRecipe = UnityHelper.GetTheChildNodeComponetScripts<Toggle>(PagingRoot, "TgRecipe");

    }
    // Start is called before the first frame update
    void Start()
    {
        recipePanel = transform.Find("recipeList").gameObject;
        herbPanel = transform.Find("herbList").gameObject;
        recipePanel.SetActive(false);
        herbPanel.SetActive(true);
        BtPrev.onClick.AddListener(() =>
            {
                MoveLeft();
            });
        BtNext.onClick.AddListener(() =>
        {
            MoveRight();
        });

        // 注册Toggle的ValueChanged事件
        TgHerb.onValueChanged.AddListener(OnToggleRecipeChanged);
        TgRecipe.onValueChanged.AddListener(OnToggleHerbsChanged);

        // 初始化时更新UI状态
        UpdateVisibility();
    }

    private void MoveLeft()
    {
        if (TgRecipe.isOn)
        {
            transform.Find("herbList").GetComponent<RecipeBackpack>().SetPrevItems(currentRecipeIndex, itemSize);
            if (currentRecipeIndex - itemSize >= 0)
            {
                currentRecipeIndex -= itemSize;
            }
        }
        else
        {
            transform.Find("herbList").GetComponent<HerbBackpack>().SetPrevItems(currentHerbIndex, itemSize);
            if (currentHerbIndex - itemSize >= 0)
            {
                currentHerbIndex -= itemSize;
                Debug.Log("prev11:" + currentHerbIndex);
            }
        }
    }

    private void MoveRight()
    {
        if (TgRecipe.isOn)
        {
            transform.Find("recipeList").GetComponent<RecipeBackpack>().SetNextItems(currentRecipeIndex, itemSize);
            if (currentRecipeIndex + itemSize < RecipeDataManager.Instance.GetAllRecipeItems().Count)
            {
                currentRecipeIndex += itemSize;
            }
        }
        else
        {
            transform.Find("herbList").GetComponent<HerbBackpack>().SetNextItems(currentHerbIndex, itemSize);
            if (currentHerbIndex + itemSize < HerbDataManager.Instance.GetAllHerbItems().Count)
            {
                currentHerbIndex += itemSize;
                Debug.Log("next11:" + currentHerbIndex);
            }
        }
    }

    void OnToggleRecipeChanged(bool isOn)
    {
        // 当药方列表的Toggle变化时调用
        if (isOn) UpdateVisibility();
    }

    void OnToggleHerbsChanged(bool isOn)
    {
        // 当药材列表的Toggle变化时调用
        if (isOn) UpdateVisibility();
    }

    void UpdateVisibility()
    {
        // 根据Toggle的状态显示或隐藏对应的Panel
        recipePanel.SetActive(TgRecipe.isOn);
        herbPanel.SetActive(TgHerb.isOn);
    }
}
