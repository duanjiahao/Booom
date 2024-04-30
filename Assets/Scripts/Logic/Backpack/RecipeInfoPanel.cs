using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FindFunc;
using UnityEngine.UI;
public class RecipeInfoPanel : MonoBehaviour
{
    public GameObject PagingRoot;
    public Button BtCreate;
    public Button BtDelete;
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
        BtCreate = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtCreate");
        BtDelete = UnityHelper.GetTheChildNodeComponetScripts<Button>(PagingRoot, "BtDelete");


    }
    // Start is called before the first frame update
    void Start()
    {
        BtCreate.onClick.AddListener(() =>
        {
            RecipeDataManager.Instance.UseRecipe(1001);

        });
        BtDelete.onClick.AddListener(() =>
        {
            RecipeDataManager.Instance.AddRecipe(1001);

        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
