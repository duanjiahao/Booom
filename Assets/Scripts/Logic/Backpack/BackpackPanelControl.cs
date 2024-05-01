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
    private Button BtPrev;
    private Button BtNext;
    public RectTransform container; // 这是你的HerbsContainer
    public float scrollAmount; // 每次翻页移动的距离
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
        if (container.localPosition.x < 0)
            container.localPosition += new Vector3(scrollAmount, 0, 0);
    }

    private void MoveRight()
    {
        if (container.localPosition.x > -GetMaxScrollAmount())
            container.localPosition -= new Vector3(scrollAmount, 0, 0);
    }

    private float GetMaxScrollAmount()
    {
        // 这里应该计算Container可移动的最大距离，依据内容总宽度和视图宽度计算
        return container.rect.width - GetComponent<RectTransform>().rect.width;
    }
}
