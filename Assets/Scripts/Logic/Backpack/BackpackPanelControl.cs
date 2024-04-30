using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FindFunc;
public class BackpackPanelControl : MonoBehaviour
{
    public GameObject PagingRoot;
    private Button BtRecipe;
    private Button BtHerb;
    private GameObject recipePanel;
    private GameObject herbPanel;
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


    }
    // Start is called before the first frame update
    void Start()
    {
        recipePanel = transform.Find("recipeList").gameObject;
        herbPanel = transform.Find("herbList").gameObject;
        recipePanel.SetActive(true);
        herbPanel.SetActive(false);
        BtRecipe.onClick.AddListener(() =>
            {
                if (!recipePanel.activeSelf)
                {
                    recipePanel.SetActive(true);
                    herbPanel.SetActive(false);
                }
                
            });
        BtHerb.onClick.AddListener(() =>
        {
            if (!herbPanel.activeSelf)
            {
                herbPanel.SetActive(true);
                recipePanel.SetActive(false);
            }

        });
    }

    private void ShowRecipe()
    {

    }
}
