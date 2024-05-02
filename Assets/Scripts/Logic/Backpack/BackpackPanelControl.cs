using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
//背包UI切换管理
public class BackpackPanelControl : MonoBehaviour
{
    public GameObject PagingRoot;
    private Button BtRecipe;
    private Button BtHerb;
    private GameObject recipePanel;
    private GameObject herbPanel;
    private string nowPanel;
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
        BtRecipe = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtRecipe");
        BtHerb = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtHerb");
        BtPrev = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtPrev");
        BtNext = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtNext");

    }
    // Start is called before the first frame update
    void Start()
    {
        recipePanel = transform.Find("recipeList").gameObject;
        herbPanel = transform.Find("herbList").gameObject;
        recipePanel.SetActive(false);
        herbPanel.SetActive(true);
        nowPanel = "herbPanel";
        BtRecipe.onClick.AddListener(() =>
            {
                if (!recipePanel.activeSelf)
                {
                    recipePanel.SetActive(true);
                    herbPanel.SetActive(false);
                    nowPanel = "recipePanel";
                }
                
            });
        BtHerb.onClick.AddListener(() =>
        {
            if (!herbPanel.activeSelf)
            {
                herbPanel.SetActive(true);
                recipePanel.SetActive(false);
                nowPanel = "herbPanel";
            }

        });
        BtPrev.onClick.AddListener(() =>
        {
            MoveLeft();

        });
        BtNext.onClick.AddListener(() =>
        {
            MoveRight();

        });
    }

    private void ShowRecipe()
    {

    }


    private void MoveLeft()
    {
        switch (nowPanel)
        {
            case "recipePanel":
                transform.Find("herbList").GetComponent<RecipeBackpack>().SetPrevItems(currentRecipeIndex, itemSize);
                if (currentRecipeIndex - itemSize >= 0)
                {
                    currentRecipeIndex -= itemSize;
                }
                break;
            case "herbPanel":
                Debug.Log("prev:" + currentHerbIndex);
                transform.Find("herbList").GetComponent<HerbBackpack>().SetPrevItems(currentHerbIndex, itemSize);
                if (currentHerbIndex - itemSize >= 0)
                {
                    currentHerbIndex -= itemSize;
                    Debug.Log("prev11:" + currentHerbIndex);
                }
                break;
        }
    }

    private void MoveRight()
    {
        switch (nowPanel)
        {
            case "recipePanel":
                transform.Find("recipeList").GetComponent<RecipeBackpack>().SetNextItems(currentRecipeIndex, itemSize);
                if (currentRecipeIndex + itemSize < RecipeDataManager.Instance.GetAllRecipeItems().Count)
                {
                    currentRecipeIndex += itemSize;
                }
                break;
            case "herbPanel":
                Debug.Log("next:" + currentHerbIndex);
                transform.Find("herbList").GetComponent<HerbBackpack>().SetNextItems(currentHerbIndex, itemSize);
                if (currentHerbIndex + itemSize < HerbDataManager.Instance.GetAllHerbItems().Count)
                {
                    currentHerbIndex += itemSize;
                    Debug.Log("next11:" + currentHerbIndex);
                }
                break;
        }
    }

    //private void MoveLeft()
    //{
    //    if (nowPanel == "herbPanel" && currentHerbIndex - itemSize >= 0)
    //    {
    //        Debug.Log("prev:" + currentHerbIndex);
    //        transform.Find("herbList").GetComponent<HerbBackpack>().SetPrevItems(currentHerbIndex, itemSize);
    //        currentHerbIndex -= itemSize;
    //        Debug.Log("prev11:" + currentHerbIndex);
    //    }
    //}

    //private void MoveRight()
    //{
    //    if (nowPanel == "herbPanel" && currentHerbIndex + itemSize < HerbDataManager.Instance.GetAllHerbItems().Count)
    //    {
    //        Debug.Log("next:" + currentHerbIndex);
    //        transform.Find("herbList").GetComponent<HerbBackpack>().SetNextItems(currentHerbIndex, itemSize);
    //        currentHerbIndex += itemSize;
    //        Debug.Log("next11:" + currentHerbIndex);
    //    }
    //}


}
