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
        //TODO 药方数据存储
        // 创建药方背包
        Inventory<BackpackRecipeItem> recipeInventory = new Inventory<BackpackRecipeItem>();
        //读取数据
        TextAsset configFile = Resources.Load<TextAsset>("Configs/HerbsConfig");
        string text = configFile.text;
        if (text != null)
        {
            //反序列化json
            var herbDict = JsonConvert.DeserializeObject<Dictionary<int, HerbsConfig>>(text);
            List<HerbsConfig> herbList = new List<HerbsConfig>(herbDict.Values);
            foreach (var item in herbList)
            {
                var obj = new BackpackRecipeItem();
                obj.InitItemInfo(item.id, item.name, item.desc, 0, new string[] { item.attribute1.ToString(), item.attribute2.ToString(), item.attribute3.ToString() });
                //将数据加入列表中
                recipeInventory.AddItem(obj);
                //Debug.Log($"Item ID: {item.id}, Name: {item.name}, Description: {item.desc}");
            }

        }
        else
        {
            Debug.LogError("Failed to load the config file.");
        }
        if (recipeInventory.GetAllItems().Count != 0)
        {
            foreach (BackpackRecipeItem data in recipeInventory.GetAllItems())
            {
                tempItem = Instantiate(
                RecipePrefab,
                transform.position,
                Quaternion.identity,
                transform
            );
                //tempItem.GetComponent<PilotInfoItem>().InitItemData(data);
                //            tempItem.GetComponent<PilotInfoItem>().SetItemData(data);

                //            PilotItemList.Add(tempItem);
                Text nameText = UnityHelper.GetTheChildNodeComponetScripts<Text>(tempItem, "name");
                nameText.text = data.Name;
            }
        }


    }
}
