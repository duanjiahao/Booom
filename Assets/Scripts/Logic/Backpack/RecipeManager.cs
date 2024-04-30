using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using UnityEngine.UI;
using FindFunc;
public class RecipeManager : MonoBehaviour
{
    public GameObject PagingRoot;
    public GameObject RecipePrefab;
    private GameObject tempItem;
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
        List<RecipeItem> recipeInventory = RecipeDataManager.Instance.recipeInventory;
        if (RecipeDataManager.Instance.GetAllRecipeItems().Count != 0)
        {
            foreach (RecipeItem data in recipeInventory)
            {
                tempItem = Instantiate(
                RecipePrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
                Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "name");
                nameText.text = data.Name;
            }
        }


    }
}
